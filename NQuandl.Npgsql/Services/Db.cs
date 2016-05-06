using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Services.Transactions;

namespace NQuandl.Npgsql.Services
{
    public class Db : IDb
    {
        private readonly IConfigureConnection _configuration;


        public Db([NotNull] IConfigureConnection configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _configuration = configuration;
        }

        public IEnumerable<IDataRecord> ExecuteQuery(string query)
        {
            using (var connection = new NpgsqlConnection(_configuration.ConnectionString))
            using (var cmd = new NpgsqlCommand(query, connection))
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

        public IObservable<IDataRecord> ExecuteQueryAsync(string query)
        {
            return Observable.Create<IDataRecord>(async obs =>
            {
                using (var connection = new NpgsqlConnection(_configuration.ConnectionString))
                using (var cmd = new NpgsqlCommand(query, connection))
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

        public async Task BulkWriteData(string sqlStatement, IObservable<List<DbData>> dataObservable)
        {
            using (var connection = new NpgsqlConnection(_configuration.ConnectionString))
            using (var importer = connection.BeginBinaryImport(sqlStatement))
            {
                await dataObservable.ForEachAsync(importData =>
                {
                    importer.StartRow();
                    foreach (var bulkImportData in importData)
                    {
                        importer.Write(bulkImportData, bulkImportData.DbType);
                    }
                });
                importer.Close();
            }
        }


     

        public async Task ExecuteCommandAsync(string command, IEnumerable<DbData> dbDatas)
        {
            var parameters = GetParameters(dbDatas);
            using (var connection = new NpgsqlConnection(_configuration.ConnectionString))
            using (var cmd = new NpgsqlCommand(command, connection))
            {
                await cmd.Connection.OpenAsync();
                cmd.Parameters.AddRange(parameters);
                cmd.Prepare();
                await cmd.ExecuteNonQueryAsync();
                cmd.Connection.Close();
            }
        }

        private NpgsqlParameter[] GetParameters(IEnumerable<DbData> dbDatas)
        {
            return dbDatas.Select(dbData => new NpgsqlParameter(dbData.ColumnName, dbData.DbType)
            {
                Value = dbData.Data,
                IsNullable = dbData.IsNullable
            }).ToArray();
        }
    }
}