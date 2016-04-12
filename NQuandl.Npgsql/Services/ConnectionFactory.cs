using System;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;

namespace NQuandl.Npgsql.Services
{
    public sealed class ConnectionFactory : IConstructConnection
    {
        private readonly IConfigureConnection _configuration;

        public ConnectionFactory([NotNull] IConfigureConnection configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _configuration = configuration;
        }

        public NpgsqlConnection ConstructConnection()
        {
            return new NpgsqlConnection(_configuration.ConnectionString);
        }
    }
}