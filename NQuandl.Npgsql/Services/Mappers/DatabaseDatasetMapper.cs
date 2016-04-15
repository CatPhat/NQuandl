using System.Data;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Services.Mappers
{
    public class DatabaseDatasetMapper : BaseDataRecordMapper<DatabaseDataset>
    {
        public override DatabaseDataset ToEntity(IDataRecord record)
        {
            return new DatabaseDataset
            {
                Id = record.GetInt32OrDefault(AttributeMetadata.GetColumnIndexByPropertName(nameof(DatabaseDataset.Id))),
                DatabaseCode =
                    record.GetStringOrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(DatabaseDataset.DatabaseCode))),
                DatasetCode =
                    record.GetStringOrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(DatabaseDataset.DatasetCode))),
                Description =
                    record.GetStringOrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(DatabaseDataset.Description)))
            };
        }
    }
}