﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Commands;

namespace NQuandl.Npgsql.Api
{
    public interface IDbContext
    {
        IEnumerable<IDataRecord> GetEnumerable(DataRecordsQuery query);
        IObservable<IDataRecord> GetObservable(DataRecordsQuery query);
        Task BulkWriteAsync(BulkWriteCommand command);
        Task WriteAsync(WriteCommand command);
        void ExecuteSqlCommand(string sqlStatement);
        Task ExecuteSqlCommandAsync(string sqlStatement);
        Task DeleteRowsAsync(DeleteCommand command);
    }
}