using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Services.Transactions;

namespace NQuandl.Npgsql.Api.Transactions
{
    public interface IEntitySqlMapper<TEntity> where TEntity : DbEntity
    {
        string BulkInsertSql();
        string GetSelectSqlBy(EntitiesReaderQuery<TEntity> query);
    }
}