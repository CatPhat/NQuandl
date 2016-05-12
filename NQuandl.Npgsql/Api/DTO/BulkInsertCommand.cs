using System;
using System.Collections.Generic;

namespace NQuandl.Npgsql.Api.DTO
{
    public class BulkInsertCommand
    {
        public string TableName { get; set; }
        public IEnumerable<ColumnNameWithIndex> ColumnNameWithIndices { get; set; }
        public IObservable<List<DbInsertData>> DbDatasObservable { get; set; }
    }
}