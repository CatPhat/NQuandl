using System;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;

namespace NQuandl.Npgsql.Services
{
    public sealed class ConnectionProvider : IProvideConnection
    {
        private readonly IConstructConnection _factory;
        private NpgsqlConnection _connection;

        public ConnectionProvider([NotNull] IConstructConnection factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));
            _factory = factory;
        }


        public NpgsqlConnection GetConnection()
        {
            if (_connection != null)
                return _connection;

            _connection = _factory.ConstructConnection();

            return _connection;
        }
    }
}