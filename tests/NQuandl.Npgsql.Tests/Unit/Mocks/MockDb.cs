﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Commands;

namespace NQuandl.Npgsql.Tests.Unit.Mocks
{
    public class MockDbContext : IDbContext
    {
        public DataRecordsQuery GetEnumerableQuery { get; private set; }
        public DataRecordsQuery GetObservableQuery { get; private set; }
        public BulkWriteCommand GetBulkWriteCommand { get; private set; }
        public WriteCommand GetWriteCommand { get; private set; }
        public DeleteCommand DeleteCommand { get; private set; }

        public IEnumerable<IDataRecord> GetEnumerable(DataRecordsQuery query)
        {
            GetEnumerableQuery = query;
            return null;
        }

        public IObservable<IDataRecord> GetObservable(DataRecordsQuery query)
        {
            GetObservableQuery = query;
            return null;
        }

        public Task BulkWriteAsync(BulkWriteCommand command)
        {
            GetBulkWriteCommand = command;
            return Task.FromResult(0);
        }

        public Task WriteAsync(WriteCommand command)
        {
            GetWriteCommand = command;
            return Task.FromResult(0);
        }

        void IDbContext.ExecuteSqlCommand(string sqlStatement)
        {
            throw new NotImplementedException();
        }

        public Task ExecuteSqlCommandAsync(string sqlStatement)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRowsAsync(DeleteCommand command)
        {
            DeleteCommand = command;
            return Task.FromResult(0);
        }

        public Task ExecuteSqlCommand(string sqlStatement)
        {
            throw new NotImplementedException();
        }
    }
}