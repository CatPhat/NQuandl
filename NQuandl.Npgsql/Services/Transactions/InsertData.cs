using System.Collections.Generic;

namespace NQuandl.Npgsql.Services.Transactions
{
    public class InsertData
    {
        public string SqlStatement { get; set; }
        public IEnumerable<DbData> DbDatas { get; set; }
    }
}