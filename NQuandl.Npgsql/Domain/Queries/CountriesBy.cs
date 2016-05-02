//using System;
//using System.Reactive.Linq;
//using System.Text;
//using JetBrains.Annotations;
//using NQuandl.Npgsql.Api;
//using NQuandl.Npgsql.Api.Transactions;
//using NQuandl.Npgsql.Domain.Entities;
//using NQuandl.Npgsql.Services.Extensions;

//namespace NQuandl.Npgsql.Domain.Queries
//{
//    public class CountriesBy : PagedResult, IDefineQuery<IObservable<Country>> {}

//    public class HandleCountriesBy :
//        IHandleQuery<CountriesBy, IObservable<Country>>
//    {
//        private readonly IMapDataRecordToEntity<Country> _mapper;
//        private readonly IExecuteRawSql _sql;

//        public HandleCountriesBy([NotNull] IExecuteRawSql sql,
//            [NotNull] IMapDataRecordToEntity<Country> mapper)
//        {
//            if (sql == null)
//                throw new ArgumentNullException(nameof(sql));
//            if (mapper == null)
//                throw new ArgumentNullException(nameof(mapper));
//            _sql = sql;
//            _mapper = mapper;
//        }

//        public IObservable<Country> Handle(CountriesBy query)
//        {
//            if (string.IsNullOrEmpty(query.OrderBy))
//            {
//                query.OrderBy = _mapper.GetColumnNameByPropertyName(dataset => dataset.Name);
//            }

//            var queryString = new StringBuilder();
//            queryString.Append($"select {_mapper} " +
//                               $"from {_mapper.GetTableName()} " +
//                               $"order by {query.OrderBy} ");

//            if (query.Limit.HasValue)
//            {
//                queryString.Append($" limit {query.Limit.Value} ");
//            }

//            if (query.Offset.HasValue)
//            {
//                queryString.Append($"offset {query.Offset.Value}");
//            }

//            var result = _sql.ExecuteQueryAsync(queryString.ToString());

//            return Observable.Create<Country>(
//                obs => result.Subscribe(
//                    record => obs.OnNext(_mapper.ToEntity(record)),
//                    onCompleted: obs.OnCompleted,
//                    onError: exception => { throw new Exception(exception.Message); }));
//        }
//    }
//}