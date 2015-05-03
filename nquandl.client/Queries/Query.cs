using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NQuandl.Client.Entities;
using NQuandl.Client.Helpers;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;

namespace NQuandl.Client.Queries
{

    public interface IDefineQuery<TResult>
    {
    }

    public interface IHandleQuery<in TQuery, out TResult> where TQuery : IDefineQuery<TResult>
    {
        TResult Handle(TQuery query);
    }

    public class EntitiesBy<TEntity> : IDefineQuery<Task<IEnumerable<TEntity>>> where TEntity : QuandlEntity
    {
        public QueryOptions Options { get; set; }
    }

    public class HandleEntitiesByQuery<TEntity> : IHandleQuery<EntitiesBy<TEntity>, Task<IEnumerable<TEntity>>> where TEntity : QuandlEntity
    {
        private readonly IQuandlService _quandlService;

        public HandleEntitiesByQuery(IQuandlService quandlService)
        {
            _quandlService = quandlService;
        } 

        public Task<IEnumerable<TEntity>> Handle(EntitiesBy<TEntity> query)
        {

            var parameters = new RequestParameters();
            

            var quandlRequest = new QuandlRequestV1(parameters);
            var response = _quandlService.GetAsync(quandlRequest);
            response.Result.Data;
        }
    }
}
