using System;
using System.Collections.Generic;
using System.Data;
using System.Reactive.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;

namespace NQuandl.Npgsql.Services
{
    public class ExecuteRawSql : IExecuteRawSql
    {
        private readonly IConfigureConnection _configuration;


        public ExecuteRawSql([NotNull] IConfigureConnection configuration)
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

        //todo seems wrong - lifetime should be handled by this class
        public NpgsqlBinaryImporter GetBulkImporter(string sqlStatement)
        {
            var connection = new NpgsqlConnection(_configuration.ConnectionString);
            return connection.BeginBinaryImport(sqlStatement);
        }

        public async Task ExecuteCommandAsync(string command, NpgsqlParameter[] parameters)
        {
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
    }
}