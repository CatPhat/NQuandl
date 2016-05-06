using System;
using System.Collections.Generic;

namespace NQuandl.Npgsql.Services.Transactions
{
    public class BulkInsertData
    {
        public string SqlStatement { get; set; }
        public IObservable<List<DbData>> DbDatasObservable { get; set; }
    }
}