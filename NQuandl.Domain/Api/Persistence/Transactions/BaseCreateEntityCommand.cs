using NQuandl.Api.Persistence.Entities;

namespace NQuandl.Api.Persistence.Transactions
{
    public abstract class BaseCreateEntityCommand<TEntity> : BaseEntityCommand where TEntity : Entity
    {
        public TEntity CreatedEntity { get; internal set; }
    }
}