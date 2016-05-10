using System.Collections.Generic;
using System.Linq;
using NQuandl.Npgsql.Api.DTO;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Services.Metadata;

namespace NQuandl.Npgsql.Services.Extensions
{
    public static class MetadataExtensions
    {
        public static IEnumerable<ColumnNameWithIndex> ToColumnNameWithIndices<TEntity>(
            this IEntityMetadataCache<TEntity> metadata) where TEntity : DbEntity
        {
            return metadata.GetPropertyInfos()
                .Select(propertyInfo => new ColumnNameWithIndex
                {
                    ColumnName = metadata.GetColumnName(propertyInfo.Name),
                    ColumnIndex = metadata.GetColumnIndex(propertyInfo.Name)
                }).OrderBy(x => x.ColumnIndex);
        }
    }
}