using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.DTO;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Commands;

namespace NQuandl.Npgsql.Services
{
    public class Db : IDb
    {
        private readonly IConfigureConnection _configuration;
        private readonly ISqlMapper _sql;

        public Db([NotNull] IConfigureConnection configuration, [NotNull] ISqlMapper sql)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            if (sql == null)
                throw new ArgumentNullException(nameof(sql));
            _configuration = configuration;
            _sql = sql;
        }

        private NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(_configuration.ConnectionString);
        }

        public IEnumerable<IDataRecord> GetEnumerable(DataRecordsQuery query)
        {
            var sqlStatement = _sql.GetSelectSqlBy(query);
            using (var connection = CreateConnection())
            using (var cmd = new NpgsqlCommand(sqlStatement, connection))
            {
                cmd.Connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return reader;
                    }
                }
                cmd.Connection.Close();
            }
        }

        public IObservable<IDataRecord> GetObservable(DataRecordsQuery query)
        {
            var sqlStatement = _sql.GetSelectSqlBy(query);
            return Observable.Create<IDataRecord>(async obs =>
            {
                using (var connection = CreateConnection())
                using (var cmd = new NpgsqlCommand(sqlStatement, connection))
                {
                    await cmd.Connection.OpenAsync();
                    using (var reader = cmd.ExecuteReaderAsync())
                    {
                        var result = await reader;
                        while (await result.ReadAsync())
                        {
                            obs.OnNext(result);
                        }
                        obs.OnCompleted();
                    }
                    cmd.Connection.Close();
                }
            });
        }

        public async Task BulkWriteAsync(BulkWriteCommand command)
        {
            var firstRow = command.DatasObservable.Take(1).Select(x => x[0]).ToEnumerable();
            var sqlStatement = _sql.GetBulkInsertSql(command.TableName, firstRow);
            using (var connection = CreateConnection())
            using (var importer = connection.BeginBinaryImport(sqlStatement))
            {
                await command.DatasObservable.ForEachAsync(importData =>
                {
                    importer.StartRow();
                    foreach (var bulkImportData in importData.OrderBy(x => x.ColumnIndex))
                    {
                        importer.Write(bulkImportData, bulkImportData.DbType);
                    }
                });
                importer.Close();
            }
        }

        public async Task WriteAsync(WriteCommand command)
        {
            var sqlStatement = _sql.GetInsertSql(command.TableName, command.Datas);

            var parameters = command.Datas.Select(dbData => new NpgsqlParameter(dbData.ColumnName, dbData.DbType)
            {
                Value = dbData.Data,
                IsNullable = dbData.IsNullable
            }).ToArray();

            using (var connection = CreateConnection())
            using (var cmd = new NpgsqlCommand(sqlStatement, connection))
            {
                await cmd.Connection.OpenAsync();
                cmd.Parameters.AddRange(parameters);
                cmd.Prepare();
                await cmd.ExecuteNonQueryAsync();
                cmd.Connection.Close();
            }
        }
    }
}