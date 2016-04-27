using System.Data;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Services.Mappers
{
    public class DatabaseStatusMapper : BaseDataRecordMapper<DatabaseStatus>
    {
        public override DatabaseStatus ToEntity(IDataRecord record)
        {
            return new DatabaseStatus
            {
                Id = record.GetInt32(AttributeMetadata.GetColumnIndexByPropertName(nameof(DatabaseStatus.Id))),
                DatabaseId =
                    record.GetInt32(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(DatabaseStatus.DatabaseId))),
                DatabaseName =
                    record.GetStringOrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(DatabaseStatus.DatabaseName))),
                DatasetsAvailable =
                    record.GetInt32(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(DatabaseStatus.DatasetsAvailable))),
                DatasetsDownloaded =
                    record.GetInt32(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(DatabaseStatus.DatasetsDownloaded))),
                DatasetLastChecked =
                    record.GetDateTime(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(DatabaseStatus.DatasetLastChecked))),
                DatabaseMarkedForDownload =
                    record.GetBoolean(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(DatabaseStatus.DatabaseMarkedForDownload)))
            };
        }
    }
}