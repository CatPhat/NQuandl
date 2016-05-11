using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Npgsql.Services.Metadata;

namespace NQuandl.Npgsql.Tests.Mocks
{
    public class MockMetadataCacheFactory
    {
        public EntityMetadataCache<MockDbEntity> CreateMockMetadataCache()
        {
            var metadataInitializer = new EntityMetadataCacheInitializer<MockDbEntity>();
            return new EntityMetadataCache<MockDbEntity>(metadataInitializer);
        }
    }
}
