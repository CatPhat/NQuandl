using System.Data;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Services.Mappers
{
    public class DatabaseDatasetMapper : BaseDataRecordMapper<DatabaseDataset>
    {
        public override DatabaseDataset ToEntity(IDataRecord record)
        {
            return new DatabaseDataset
            {
                Id = record.GetInt32(AttributeMetadata.GetColumnIndexByPropertName(nameof(DatabaseDataset.Id))),
                DatabaseCode =
                    record.GetString(AttributeMetadata.GetColumnIndexByPropertName(nameof(DatabaseDataset.DatabaseCode))),
                DatasetCode =
                    record.GetString(AttributeMetadata.GetColumnIndexByPropertName(nameof(DatabaseDataset.DatasetCode))),
                Description =
                    record.GetString(AttributeMetadata.GetColumnIndexByPropertName(nameof(DatabaseDataset.Description)))
            };
        }
    }
}