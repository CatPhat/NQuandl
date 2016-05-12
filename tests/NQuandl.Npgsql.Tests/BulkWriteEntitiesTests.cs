using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NQuandl.Npgsql.Domain.Commands;
using NQuandl.Npgsql.Tests.Mocks;
using Xunit;

namespace NQuandl.Npgsql.Tests
{
    public class BulkWriteEntitiesTests
    {
        [Fact]
        public async void DbInsertDatasAreValidTest()
        {
            const int upperLimit = 1000;
            var entitiesToInsert = new List<MockDbEntity>();
            for (var i = 0; i < upperLimit; i++)
            {
                var entity = new MockDbEntity
                {
                    Id = i,
                    InsertDate = DateTime.Now,
                    Name = $"nameValueWithId{i}"
                };
                entitiesToInsert.Add(entity);
            }
            var metadata = MockMetadataFactory<MockDbEntity>.Metadata;
            var mockDb = new MockDb();
            
            var bulkWriteEntityCommand = new BulkWriteEntities<MockDbEntity>(entitiesToInsert);
            var bulkWriteEntityHandler = new HandleBulkWriteEntities<MockDbEntity>(metadata, mockDb).Handle(bulkWriteEntityCommand);

            var importDatas = await mockDb.GetBulkWriteCommand.DatasObservable.ToList();

            for (int i = 0; i < upperLimit; i++)
            {
                Assert.Equal(entitiesToInsert[i].Id, importDatas[i][0].Data);
                Assert.Equal(entitiesToInsert[i].Name, importDatas[i][1].Data);
                Assert.Equal(entitiesToInsert[i].InsertDate, importDatas[i][2].Data);


            }
     
          

        }
    }
}
