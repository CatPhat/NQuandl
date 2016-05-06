using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Npgsql;
using NQuandl.Npgsql.Services.Transactions;

namespace NQuandl.Npgsql.Api
{
    public interface IExecuteRawSql
    {
        IEnumerable<IDataRecord> ExecuteQuery(string query);
        IObservable<IDataRecord> ExecuteQueryAsync(string query);
        Task BulkWriteData(string sqlStatement, IObservable<List<DbData>> dataObservable);
        Task ExecuteCommandAsync(string command, NpgsqlParameter[] parameters);
    }
}