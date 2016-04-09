using System;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Services.Npgsql.Api;

namespace NQuandl.Services.Npgsql
{
    internal sealed class ConnectionFactory : IConstructConnection
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