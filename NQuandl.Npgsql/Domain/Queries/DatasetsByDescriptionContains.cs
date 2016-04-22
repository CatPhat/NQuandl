using System;
using System.Reactive.Linq;
using System.Text;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Domain.Queries
{
    public class DatasetsByDescriptionContains : PagedResult, IDefineQuery<IObservable<Dataset>>
    {
        public string QueryString { get; set; }

        public DatasetsByDescriptionContains(string queryString)
        {
            QueryString = queryString;
        }
    }

    public class HandleDatasetsByDescriptionContains :
        IHandleQuery<DatasetsByDescriptionContains, IObservable<Dataset>>
    {
        private readonly IMapDataRecordToEntity<Dataset> _mapper;
        private readonly IExecuteRawSql _sql;

        public HandleDatasetsByDescriptionContains([NotNull] IExecuteRawSql sql,
            [NotNull] IMapDataRecordToEntity<Dataset> mapper)
        {
            if (sql == null)
                throw new ArgumentNullException(nameof(sql));
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));
            _sql = sql;
            _mapper = mapper;
        }

        public IObservable<Dataset> Handle(DatasetsByDescriptionContains query)
        {
            
            if (string.IsNullOrEmpty(query.OrderBy))
            {
                query.OrderBy = _mapper.GetColumnNameByPropertyName(dataset => dataset.Code);
            }

            var queryString = new StringBuilder();
            queryString.Append($"select {_mapper.GetColumnNames()} " +
                               $"from {_mapper.GetTableName()} " +
                               $"where {_mapper.GetColumnNameByPropertyName(dataset => dataset.DatabaseCode)} " +
                               $"like '% {query.QueryString} %'" +
                               $"order by {query.OrderBy} ");


            if (query.Limit.HasValue)
            {
                queryString.Append($" limit {query.Limit.Value} ");
            }

            if (query.Offset.HasValue)
            {
                queryString.Append($"offset {query.Offset.Value}");
            }

            var result = _sql.ExecuteQueryAsync(queryString.ToString());

            return Observable.Create<Dataset>(
                obs => result.Subscribe(
                    record => obs.OnNext(_mapper.ToEntity(record)), onCompleted: obs.OnCompleted, onError:
                    exception => { throw new Exception(exception.Message); }));
        }
    }
}