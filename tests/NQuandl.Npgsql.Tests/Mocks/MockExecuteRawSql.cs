using System;
using System.Collections.Generic;
using System.Data;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Npgsql;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.DTO;
using NQuandl.Npgsql.Services.Transactions;

namespace NQuandl.Npgsql.Tests.Mocks
{
    public class MockDb : IDb
    {
        public MockDb()
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

        public async Task BulkWriteData(string sqlStatement, IObservable<List<DbImportData>> dataObservable)
        {
            await dataObservable.ForEachAsync(x =>
            {
                foreach (var bulkImportData in x)
                {
                    AddToImportedData(bulkImportData);
                }
            });
        }

        public Task ExecuteCommandAsync(string command, IEnumerable<DbImportData> dbDatas)
        {
            throw new NotImplementedException();
        }


      

        private void AddToImportedData(DbImportData importImportData)
        {
            ImportedData.Add(new MockBulkImportOrder
            {
                ColumnIndex = CurrentColumnIndex,
                RowIndex = CurrentRowIndex,
                Data = importImportData.Data,
                DbType = importImportData.DbType
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
            Console.WriteLine(importImportData.Data);
        }
    }
}