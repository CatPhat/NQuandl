using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using NpgsqlTypes;
using NQuandl.Npgsql.Domain.Commands;
using NQuandl.Npgsql.Tests.Mocks;
using Xunit;

namespace NQuandl.Npgsql.Tests
{
    public class CommandTests
    {
        [Fact]
        public async void BulkWriteEntityTest()
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

            var metadata = MockMetadataFactory.Metadata;
            var mockDb = new MockDb();

            var bulkWriteEntityCommand = new BulkWriteEntities<MockDbEntity>(entitiesToInsert);
            var bulkWriteEntityHandler =
                new HandleBulkWriteEntities<MockDbEntity>(metadata, mockDb).Handle(bulkWriteEntityCommand);
            var importDatas = await mockDb.GetBulkWriteCommand.DatasObservable.ToList();

            for (var i = 0; i < upperLimit; i++)
            {
                var importDatasList = importDatas[i].ToList();

                var column0 = importDatasList[0];
                Assert.Equal(entitiesToInsert[i].Id, column0.Data);
                Assert.Equal(0, column0.ColumnIndex);
                Assert.Equal(NpgsqlDbType.Integer, column0.DbType);
                Assert.Equal("id", column0.ColumnName);
                Assert.Equal(false, column0.IsNullable);
                Assert.Equal(false, column0.IsStoreGenerated);

                var column1 = importDatasList[1];
                Assert.Equal(entitiesToInsert[i].Name, column1.Data);
                Assert.Equal(1, column1.ColumnIndex);
                Assert.Equal(NpgsqlDbType.Text, column1.DbType);
                Assert.Equal("name", column1.ColumnName);
                Assert.Equal(true, column1.IsNullable);
                Assert.Equal(false, column1.IsStoreGenerated);

                var column2 = importDatasList[2];
                Assert.Equal(entitiesToInsert[i].InsertDate, column2.Data);
                Assert.Equal(2, column2.ColumnIndex);
                Assert.Equal(NpgsqlDbType.Timestamp, column2.DbType);
                Assert.Equal("insert_date", column2.ColumnName);
                Assert.Equal(false, column2.IsNullable);
                Assert.Equal(false, column2.IsStoreGenerated);
            }
        }

        [Fact]
        public void WriteEntityTest()
        {
            const int id = 777;
            const string name = "nameValue";
            var instertDate = DateTime.Now;

            var entity = new MockDbEntity
            {
                Id = id,
                InsertDate = DateTime.Now,
                Name = name
            };

            var metadata = MockMetadataFactory.Metadata;
            var mockDb = new MockDb();

            var bulkWriteEntityCommand = new WriteEntity<MockDbEntity>(entity);
            var bulkWriteEntityHandler =
                new HandleWriteEntity<MockDbEntity>(mockDb, metadata).Handle(bulkWriteEntityCommand);
            var importDatas = mockDb.GetWriteCommand.Datas.ToList();

            var column0 = importDatas[0];
            Assert.Equal(id, column0.Data);
            Assert.Equal(0, column0.ColumnIndex);
            Assert.Equal(NpgsqlDbType.Integer, column0.DbType);
            Assert.Equal("id", column0.ColumnName);
            Assert.Equal(false, column0.IsNullable);
            Assert.Equal(false, column0.IsStoreGenerated);

            var column1 = importDatas[1];
            Assert.Equal(name, column1.Data);
            Assert.Equal(1, column1.ColumnIndex);
            Assert.Equal(NpgsqlDbType.Text, column1.DbType);
            Assert.Equal("name", column1.ColumnName);
            Assert.Equal(true, column1.IsNullable);
            Assert.Equal(false, column1.IsStoreGenerated);

            var column2 = importDatas[2];
            Assert.Equal(instertDate, column2.Data);
            Assert.Equal(2, column2.ColumnIndex);
            Assert.Equal(NpgsqlDbType.Timestamp, column2.DbType);
            Assert.Equal("insert_date", column2.ColumnName);
            Assert.Equal(false, column2.IsNullable);
            Assert.Equal(false, column2.IsStoreGenerated);
        }
    }
}