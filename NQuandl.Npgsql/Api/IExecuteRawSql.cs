using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace NQuandl.Npgsql.Api
{
    public interface IExecuteRawSql
    {
        IEnumerable<IDataRecord> ExecuteQuery(string query);
        IObservable<IDataRecord> ExecuteQueryAsync(string query);
        NpgsqlBinaryImporter GetBulkImporter(string sqlStatement);
        Task ExecuteCommandAsync(string command, NpgsqlParameter[] parameters);
    }
}