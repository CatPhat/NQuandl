using System.Data;

namespace NQuandl.Npgsql.Api.Entities
{
    public interface IMapDataRecordToEntity
    {
        TEntity ToEntity<TEntity>(IDataRecord record);
    }
}