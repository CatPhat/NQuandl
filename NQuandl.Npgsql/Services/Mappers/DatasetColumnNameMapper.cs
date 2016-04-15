using System.Data;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Services.Mappers
{
    public class DatasetColumnNameMapper : BaseDataRecordMapper<DatasetColumnName>
    {
        public override DatasetColumnName ToEntity(IDataRecord record)
        {
            return new DatasetColumnName
            {
                Id = record.GetInt32(AttributeMetadata.GetColumnIndexByPropertName(nameof(DatasetColumnName.Id))),
                ColumnIndex = record.GetInt32(AttributeMetadata.GetColumnIndexByPropertName(nameof(DatasetColumnName.ColumnIndex))),
                ColumnName = record.GetStringOrDefault(AttributeMetadata.GetColumnIndexByPropertName(nameof(DatasetColumnName.ColumnName))),
                DatasetId = record.GetInt32(AttributeMetadata.GetColumnIndexByPropertName(nameof(DatasetColumnName.DatasetId))),
               
            };
        }
    }
}