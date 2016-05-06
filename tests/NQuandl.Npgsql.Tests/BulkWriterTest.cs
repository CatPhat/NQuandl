using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using NpgsqlTypes;
using NQuandl.Npgsql.Services.Mappers;
using NQuandl.Npgsql.Services.Metadata;
using NQuandl.Npgsql.Services.Transactions;
using NQuandl.Npgsql.Tests.Mocks;
using Xunit;

namespace NQuandl.Npgsql.Tests
{
    public class BulkWriterTest
    {
        [Fact]
        public async void BulkWriteEntitiesTest()
        {
            var sqlMapper = new EntitySqlMapper<MockDbEntity>(new EntityMetadata<MockDbEntity>());
            var mockdb = new MockDb();
            var entityWriter = new EntityWriter<MockDbEntity>(sqlMapper, mockdb);

            var mockDbEntites = new List<MockDbEntity>();
            for (var i = 0; i < 3; i++)
            {
                mockDbEntites.Add(new MockDbEntity
                {
                    Id = i,
                    InsertDate = DateTime.Now,
                    Name = $"nameValue{i}"
                });
            }
            await entityWriter.BulkWriteEntities(mockDbEntites.ToObservable());

            for (var i = 0; i < 3; i++)
            {
                var i1 = i;
                var row = mockdb.ImportedData.Where(x => x.RowIndex == i1);
                var mockBulkImportOrders = row as IList<MockBulkImportOrder> ?? row.ToList();
                var column0 = mockBulkImportOrders.First(x => x.ColumnIndex == 0);
                var column1 = mockBulkImportOrders.First(x => x.ColumnIndex == 1);
                var column2 = mockBulkImportOrders.First(x => x.ColumnIndex == 2);

                Assert.Equal(mockDbEntites[i].Id, column0.Data);
                Assert.Equal(NpgsqlDbType.Integer, column0.DbType);

                Assert.Equal(mockDbEntites[i].Name, column1.Data);
                Assert.Equal(NpgsqlDbType.Text, column1.DbType);

                Assert.Equal(mockDbEntites[i].InsertDate, column2.Data);
                Assert.Equal(NpgsqlDbType.Timestamp, column2.DbType);
            }
        }
    }
}