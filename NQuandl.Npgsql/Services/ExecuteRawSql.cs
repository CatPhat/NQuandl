using System;
using System.Data.Common;
using System.Threading.Tasks;
using Npgsql;
using NQuandl.Npgsql.Api;

namespace NQuandl.Npgsql.Services
{
    public class ExecuteRawSql : IExecuteRawSql
    {
        private readonly IProvideConnection _connection;


        public ExecuteRawSql(IProvideConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));
            _connection = connection;
        }

        public async Task<DbDataReader> ExecuteQuery(string query)
        {
            using (var connection = _connection.GetConnection())
            using (var cmd = new NpgsqlCommand(query, connection))
            {
                //cmd.Connection.Open();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    return reader;
                }
            }
        }

        public async Task ExecuteCommand(string command)
        {
            using (var connection = _connection.GetConnection())
            using (var cmd = new NpgsqlCommand(command, connection))
            {
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}