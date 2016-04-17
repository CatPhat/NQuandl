﻿using System;
using System.Reactive.Linq;
using System.Text;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Domain.Queries
{
    public class DatabasesBy : PagedResult, IDefineQuery<IObservable<Database>> {}

    public class HandleDatabasesBy :
        IHandleQuery<DatabasesBy, IObservable<Database>>
    {
        private readonly IMapDataRecordToEntity<Database> _mapper;
        private readonly IExecuteRawSql _sql;

        public HandleDatabasesBy([NotNull] IExecuteRawSql sql,
            [NotNull] IMapDataRecordToEntity<Database> mapper)
        {
            if (sql == null)
                throw new ArgumentNullException(nameof(sql));
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));
            _sql = sql;
            _mapper = mapper;
        }

        public IObservable<Database> Handle(DatabasesBy query)
        {
            if (string.IsNullOrEmpty(query.OrderBy))
            {
                query.OrderBy = _mapper.GetColumnNameByPropertyName(dataset => dataset.DatabaseCode);
            }

            var queryString = new StringBuilder();
            queryString.Append($"select {_mapper.GetColumnNames()} " +
                               $"from {_mapper.GetTableName()} " +
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

            return Observable.Create<Database>(
                obs => result.Subscribe(
                    record => obs.OnNext(_mapper.ToEntity(record)),
                    exception => { throw new Exception(exception.Message); }));
        }
    }
}