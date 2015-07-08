using System;
using NQuandl.Client.Api;

namespace NQuandl.Client.Domain.Queries
{
    public class GetQuandlCodeByEntity<TEntity> : IDefineQuery<string> where TEntity : QuandlEntity
    {
    }

    public class HandleGetQuandlCodeByEntity<TEntity> : IHandleQuery<GetQuandlCodeByEntity<TEntity>, string>
        where TEntity : QuandlEntity
    {
        public string Handle(GetQuandlCodeByEntity<TEntity> query)
        {
            var entity = (TEntity) Activator.CreateInstance(typeof (TEntity));
            return entity.QuandlCode;
        }
    }
}