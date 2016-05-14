using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Services.Metadata;
using NQuandl.Npgsql.Tests.Unit.Mocks;

namespace NQuandl.Npgsql.Tests.Unit._Fixtures
{
    public class MockMetadataFixture
    {
        public IEntityMetadataCache<MockDbEntity> Metadata { get; }

        public MockMetadataFixture()
        {
            Metadata = new MockMetadataFixture<MockDbEntity>().Metadata;
        }
    }

    public class MockMetadataFixture<TEntity> where TEntity : DbEntity
    {
        public IEntityMetadataCache<TEntity> Metadata { get; }

        public MockMetadataFixture()
        {
            var metadataInitializer = new EntityMetadataCacheInitializer<TEntity>();

            Metadata = new EntityMetadataCache<TEntity>(metadataInitializer);
        }
    }
}