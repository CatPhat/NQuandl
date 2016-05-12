using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Services.Metadata;

namespace NQuandl.Npgsql.Tests.Mocks
{
    public static class MockMetadataFactory<TEntity> where TEntity : DbEntity
    {
        public static IEntityMetadataCache<TEntity> Metadata;

        static MockMetadataFactory()
        {
            var metadataInitializer = new EntityMetadataCacheInitializer<TEntity>();
           
            Metadata = new EntityMetadataCache<TEntity>(metadataInitializer);
        }
    }
}