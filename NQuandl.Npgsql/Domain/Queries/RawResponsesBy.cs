//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reactive.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using JetBrains.Annotations;
//using NQuandl.Npgsql.Api;
//using NQuandl.Npgsql.Api.Transactions;
//using NQuandl.Npgsql.Domain.Entities;
//using NQuandl.Npgsql.Services.Extensions;
//using NQuandl.Npgsql.Services.Helpers;

//namespace NQuandl.Npgsql.Domain.Queries
//{
//    public class RawResponsesBy : PagedResult, IDefineQuery<IObservable<RawResponse>>
//    {

//    }

//    public class HandleRawResponsesBy : IHandleQuery<RawResponsesBy, IObservable<RawResponse>>
//    {
//        private readonly IExecuteRawSql _sql;
//        private readonly IMapDataRecordToEntity<RawResponse> _mapper;

//        public HandleRawResponsesBy([NotNull] IExecuteRawSql sql, [NotNull] IMapDataRecordToEntity<RawResponse> mapper)
//        {
//            if (sql == null)
//                throw new ArgumentNullException(nameof(sql));
//            if (mapper == null)
//                throw new ArgumentNullException(nameof(mapper));
//            _sql = sql;
//            _mapper = mapper;
//        }

//        public IObservable<RawResponse> Handle(RawResponsesBy query)
//        {
//            if (string.IsNullOrEmpty(query.OrderBy))
//            {
//                //todo change func from string to object
//                query.OrderBy = "id";
//            }

//            var queryString = new StringBuilder();
//            queryString.Append($"select {_mapper.GetColumnNames()} " +
//                               $"from {_mapper.GetTableName()} " +
//                               $"where {_mapper.GetColumnNameByPropertyName(x => x.RequestUri)} is not null " +
//                               $"order by {query.OrderBy}");


//            if (query.Limit.HasValue)
//            {
//                queryString.Append($" limit {query.Limit.Value} ");
//            }

//            if (query.Offset.HasValue)
//            {
//                queryString.Append($"offset {query.Offset.Value}");
//            }

//            var sqlStatement = queryString.ToString();
//            var result = _sql.ExecuteQueryAsync(sqlStatement);

//            return Observable.Create<RawResponse>(
//                obs => result.Subscribe(
//                    record => obs.OnNext(_mapper.ToEntity(record)), onCompleted: obs.OnCompleted ,onError:
//                    exception => { throw new Exception(exception.Message); }));


//        }
//    }
//}
