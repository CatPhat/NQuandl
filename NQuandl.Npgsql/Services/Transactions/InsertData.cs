using System.Collections.Generic;
using Npgsql;

namespace NQuandl.Npgsql.Services.Transactions
{
    public class InsertData
    {
        public string SqlStatement { get; set; }
        public NpgsqlParameter[] Parameters { get; set; }
    }
}