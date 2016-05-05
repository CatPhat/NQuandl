using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using NQuandl.Npgsql.Api;
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
            var mockdb = new MockExecuteRawSql();
            var entityWriter = new EntityWriter<MockDbEntity>(sqlMapper,
                new EntityMetadata<MockDbEntity>(), mockdb
                );

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

    public class MockExecuteRawSql : IExecuteRawSql
    {
        public MockExecuteRawSql()
        {
            ImportedData = new List<MockBulkImportOrder>();
            CurrentColumnIndex = 0;
            CurrentRowIndex = 0;
        }

        private int CurrentColumnIndex { get; set; }
        private int CurrentRowIndex { get; set; }
        public List<MockBulkImportOrder> ImportedData { get; set; }

        public IEnumerable<IDataRecord> ExecuteQuery(string query)
        {
            throw new NotImplementedException();
        }

        public IObservable<IDataRecord> ExecuteQueryAsync(string query)
        {
            throw new NotImplementedException();
        }

        public async Task BulkWriteData(string sqlStatement, IObservable<IEnumerable<BulkImportData>> dataObservable)
        {
            await dataObservable.ForEachAsync(x =>
            {
                foreach (var bulkImportData in x)
                {
                    AddToImportedData(bulkImportData);
                }
            });
        }


        public Task ExecuteCommandAsync(string command, NpgsqlParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        private void AddToImportedData(BulkImportData importData)
        {
            ImportedData.Add(new MockBulkImportOrder
            {
                ColumnIndex = CurrentColumnIndex,
                RowIndex = CurrentRowIndex,
                Data = importData.Data,
                DbType = importData.DbType
            });

            CurrentColumnIndex = CurrentColumnIndex + 1;

            if (CurrentColumnIndex != 3)
                return;

            if (CurrentColumnIndex > 3)
            {
                throw new Exception($"columnIndex: {CurrentColumnIndex} greater than 3.");
            }
            CurrentColumnIndex = 0;
            CurrentRowIndex = CurrentRowIndex + 1;
            Console.WriteLine(importData.Data);
        }
    }

    public class MockBulkImportOrder
    {
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public object Data { get; set; }
        public NpgsqlDbType DbType { get; set; }
    }
}