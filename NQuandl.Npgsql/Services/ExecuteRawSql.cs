using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;

namespace NQuandl.Npgsql.Services
{
    public class ExecuteRawSql : IExecuteRawSql
    {
        private readonly IConfigureConnection _configuration;
      


        public ExecuteRawSql([NotNull] IConfigureConnection _configuration)
        {
            if (_configuration == null)
                throw new ArgumentNullException(nameof(_configuration));
            this._configuration = _configuration;
      
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
            }





        }

        public async Task ExecuteCommand(string command)
        {
            //using (var connection = _connection.GetConnection())
            //using (var cmd = new NpgsqlCommand(command, connection))
            //{
            //    await cmd.ExecuteNonQueryAsync();
            //}
        }
    }
}