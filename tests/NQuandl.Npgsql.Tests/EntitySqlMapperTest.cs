using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Npgsql.Services;
using NQuandl.Npgsql.Services.Mappers;
using NQuandl.Npgsql.Services.Metadata;
using NQuandl.Npgsql.Services.Transactions;
using NQuandl.Npgsql.Tests.Mocks;
using Xunit;

namespace NQuandl.Npgsql.Tests
{
    public class EntitySqlMapperTest
    {
        [Fact]
        public void EntityBulkMapSqlStatementTest()
        {
            var metadataProvider = new EntityMetadataProvider<MockDbEntity>();
            var metadata = new EntityMetadata<MockDbEntity>(metadataProvider);
            var sql = new EntitySqlMapper<MockDbEntity>(metadata);

            var statement = sql.BulkInsertSql();
            Assert.Equal(statement, "");

        }
    }
}
