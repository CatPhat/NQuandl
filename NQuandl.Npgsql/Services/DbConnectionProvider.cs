using System;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;

namespace NQuandl.Npgsql.Services
{
    public class DbConnectionProvider : IProvideConnection
    {
        private readonly IConfigureConnection _configuration;

        public DbConnectionProvider([NotNull] IConfigureConnection configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _configuration = configuration;
        }

        public NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(_configuration.ConnectionString);
        }
    }
}