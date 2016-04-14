using System.Data;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Api
{
    public interface IMapDataRecordToEntity<out TEntity>
    {
        TEntity ToEntity(IDataRecord record);
        DbEntityAttributeMetadata AttributeMetadata { get; }
    }

    public abstract class BaseDataRecordMapper<TEntity> : IMapDataRecordToEntity<TEntity>
    {
        protected BaseDataRecordMapper()
        {
            AttributeMetadata = DbAttributeCache<TEntity>.AttributeAttributeMetadata;
        }

        public abstract TEntity ToEntity(IDataRecord record);
        public DbEntityAttributeMetadata AttributeMetadata { get; }
       // protected readonly DbEntityAttributeMetadata AttributeAttributeMetadata;
    }
}