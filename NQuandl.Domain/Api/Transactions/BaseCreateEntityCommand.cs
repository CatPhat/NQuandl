using NQuandl.Api.Entities;

namespace NQuandl.Api.Transactions
{
    public abstract class BaseCreateEntityCommand<TEntity> : BaseEntityCommand where TEntity : Entity
    {
        public TEntity CreatedEntity { get; internal set; }
    }
}