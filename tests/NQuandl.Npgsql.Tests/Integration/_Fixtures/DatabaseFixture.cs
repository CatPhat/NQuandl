using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Services.Database;
using NQuandl.Npgsql.Services.Database.Configuration;
using NQuandl.Npgsql.Services.Mappers;

namespace NQuandl.Npgsql.Tests.Integration._Fixtures
{
    public class DatabaseFixture
    {
        public DatabaseFixture()
        {

            var configuration = new DebugConnectionConfiguration();
            var connection = new DbConnectionProvider(configuration);
            var sqlMapper = new SqlMapper();
            Connection = connection;
            DbContext = new DbContex(connection, sqlMapper);
        }
        public IProvideDbConnection Connection { get; }
        public IDbContext DbContext { get; }
    }
}