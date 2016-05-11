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

        public NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(_configuration.ConnectionString);
        }

      

     

     

        
       
    }
}