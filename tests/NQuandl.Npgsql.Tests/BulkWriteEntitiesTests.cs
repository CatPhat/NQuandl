using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
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
            const int upperLimit = 10;
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
            var metadataSw = new Stopwatch();
            var bulkWriteCommandSw = new Stopwatch();
            var bulkWriteHandleSw = new Stopwatch();
            var importDatasSw = new Stopwatch();

            metadataSw.Start();
            var metadata = MockMetadataFactory<MockDbEntity>.Metadata;
            var mockDb = new MockDb();
            metadataSw.Stop();

            bulkWriteCommandSw.Start();
            var bulkWriteEntityCommand = new BulkWriteEntities<MockDbEntity>(entitiesToInsert);
            bulkWriteCommandSw.Stop();

            bulkWriteHandleSw.Start();
            var bulkWriteEntityHandler =
                new HandleBulkWriteEntities<MockDbEntity>(metadata, mockDb).Handle(bulkWriteEntityCommand);
            bulkWriteHandleSw.Stop();

            importDatasSw.Start();
            var importDatas = await mockDb.GetBulkWriteCommand.DatasObservable.ToList();
            importDatasSw.Stop();
            for (var i = 0; i < upperLimit; i++)
            {
                var importDatasList = importDatas[i].ToList();
                Assert.Equal(entitiesToInsert[i].Id, importDatasList[0].Data);
                Assert.Equal(entitiesToInsert[i].Name, importDatasList[1].Data);
                Assert.Equal(entitiesToInsert[i].InsertDate, importDatasList[2].Data);
            }
        }
    }
}