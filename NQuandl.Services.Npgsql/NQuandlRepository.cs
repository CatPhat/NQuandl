using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Services.Npgsql.Api;

namespace NQuandl.Services.Npgsql
{
    public class NQuandlRepository
    {
        private readonly IProvideConnection _connection;


        public NQuandlRepository(IProvideConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));
            _connection = connection;
        }

        public IList<Ttable> Query<Ttable>() where Ttable : DbTable
        {
            using (var connection = _connection.GetConnection())
            {
                
            }
        }
    }
}