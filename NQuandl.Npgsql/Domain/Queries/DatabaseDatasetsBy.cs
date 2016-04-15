using System;
using System.Reactive.Linq;
using System.Text;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Extensions;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Domain.Queries
{
    public abstract class PagedResult
    {
        public int? Offset { get; set; }
        public int? Limit { get; set; }
        public string OrderBy { get; set; }
    }

    public class DatabaseDatasetsByDatabaseCode : PagedResult, IDefineQuery<IObservable<DatabaseDataset>>
    {
        public DatabaseDatasetsByDatabaseCode(string databaseCode)
        {
            DatabaseCode = databaseCode;
        }

        public string DatabaseCode { get; }
    }

    public class HandleDatabaseDatasetsByDatabaseCode :
        IHandleQuery<DatabaseDatasetsByDatabaseCode, IObservable<DatabaseDataset>>
    {
        private readonly IMapDataRecordToEntity<DatabaseDataset> _mapper;
        private readonly IExecuteRawSql _sql;

        public HandleDatabaseDatasetsByDatabaseCode([NotNull] IExecuteRawSql sql,
            [NotNull] IMapDataRecordToEntity<DatabaseDataset> mapper)
        {
            if (sql == null)
                throw new ArgumentNullException(nameof(sql));
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));
            _sql = sql;
            _mapper = mapper;
        }

        public IObservable<DatabaseDataset> Handle(DatabaseDatasetsByDatabaseCode query)
        {
            var databaseCode = query.DatabaseCode.ToUpperInvariant();

            if (string.IsNullOrEmpty(query.OrderBy))
            {
                query.OrderBy = _mapper.GetColumnNameByPropertyName(dataset => dataset.DatabaseCode);
            }

            var queryString = new StringBuilder();
            queryString.Append($"select {_mapper.GetColumnNames()} " +
                               $"from {_mapper.GetTableName()} " +
                               $"where {_mapper.GetColumnNameByPropertyName(dataset => dataset.DatabaseCode)} " +
                               $"= '{databaseCode}'" +
                               $"order by {query.OrderBy} ");


            if (query.Limit.HasValue && query.Offset.HasValue)
            {
                queryString.Append($"limit {query.Limit.Value} offset {query.Offset.Value}");
            }

            var result = _sql.ExecuteQueryAsync(queryString.ToString());

            return Observable.Create<DatabaseDataset>(
                obs => result.Subscribe(
                    record => obs.OnNext(_mapper.ToEntity(record)),
                    exception => { throw new Exception(exception.Message); }));
        }
    }
}