using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace NQuandl.Npgsql.Api
{
    public interface IExecuteRawSql
    {
        IEnumerable<IDataRecord> ExecuteQuery(string query);
        Task ExecuteCommand(string command);
    }
}