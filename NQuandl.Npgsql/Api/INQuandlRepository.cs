using System.Data.Common;
using System.Threading.Tasks;

namespace NQuandl.Npgsql.Api
{
    public interface IExecuteRawSql
    {
        Task<DbDataReader> ExecuteQuery(string query);
        Task ExecuteCommand(string command);
    }
}