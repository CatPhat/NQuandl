using System;
using System.Collections.Generic;
using NQuandl.Npgsql.Api.DTO;

namespace NQuandl.Npgsql.Domain.Commands
{
    public class BulkWriteCommand
    {
        public string TableName { get; set; }
        public IObservable<IEnumerable<DbInsertData>> DatasObservable { get; set; }
    }
}