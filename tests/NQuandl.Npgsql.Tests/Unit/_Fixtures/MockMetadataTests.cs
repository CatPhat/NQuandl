using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Tests.Unit.Mocks;
using Xunit;

namespace NQuandl.Npgsql.Tests.Unit._Fixtures
{
    public abstract class MockMetadataTests : IClassFixture<MockMetadataFixture>
    {
        protected MockMetadataTests(MockMetadataFixture mockMetadata)
        {
            MockMetadata = mockMetadata.Metadata;
        }

        public IEntityMetadataCache<MockDbEntity> MockMetadata { get; }
    }
}