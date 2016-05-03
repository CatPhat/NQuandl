using System;
using System.Data;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;

namespace NQuandl.Npgsql.Services.Mappers
{
    public class EntityColumnTypeMapper<TEntity> where TEntity : DbEntity, new()
    {
        private readonly IEntityMetadata<TEntity> _metadata;

        public EntityColumnTypeMapper([NotNull] IEntityMetadata<TEntity> metadata)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            _metadata = metadata;
        }

        public TEntity ToEntity(IDataRecord record)
        {
            var entity = (TEntity) Activator.CreateInstance(typeof (TEntity), new object[] {});

            foreach (var dbEntityPropertyMetadata in _metadata.GetProperyNameDbMetadata())
            {
                var entityProperty = dbEntityPropertyMetadata.Value.PropertyInfo;
                var recordValue = record[dbEntityPropertyMetadata.Value.ColumnIndex];
                if (recordValue != DBNull.Value)
                {
                    entityProperty.SetValue(entity, recordValue);
                }
            }

            return entity;
        }
    }
}