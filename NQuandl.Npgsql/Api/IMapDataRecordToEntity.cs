using System.Data;

namespace NQuandl.Npgsql.Api
{
    public interface IMapDataRecordToEntity
    {
        TEntity ToEntity<TEntity>(IDataRecord record);
    }
}