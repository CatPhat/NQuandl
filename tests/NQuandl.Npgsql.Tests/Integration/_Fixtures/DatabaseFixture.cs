using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Database;
using NQuandl.Npgsql.Services.Database.Configuration;
using NQuandl.Npgsql.Services.Mappers;
using NQuandl.Npgsql.Services.Metadata;

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
            DbContext = new DbContext(connection, sqlMapper);

            CountryMetadataCache = new EntityMetadataCache<Country>(new EntityMetadataCacheInitializer<Country>());
        }
        public IProvideDbConnection Connection { get; }
        public IDbContext DbContext { get; }
        public IEntityMetadataCache<Country>  CountryMetadataCache { get; }
        
    }
}