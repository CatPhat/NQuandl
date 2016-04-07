using NQuandl.Domain.Persistence.Api.Entities;

namespace NQuandl.Domain.Persistence.Api.Transactions
{
    public abstract class BaseCreateEntityCommand<TEntity> : BaseEntityCommand where TEntity : Entity
    {
        public TEntity CreatedEntity { get; internal set; }
    }
}