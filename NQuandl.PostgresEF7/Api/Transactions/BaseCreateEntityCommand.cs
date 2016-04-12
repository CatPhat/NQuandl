using NQuandl.PostgresEF7.Api.Entities;

namespace NQuandl.PostgresEF7.Api.Transactions
{
    public abstract class BaseCreateEntityCommand<TEntity> : BaseEntityCommand where TEntity : Entity
    {
        public TEntity CreatedEntity { get; internal set; }
    }
}