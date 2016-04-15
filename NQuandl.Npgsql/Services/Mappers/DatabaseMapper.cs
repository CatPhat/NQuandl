using System.Data;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Services.Mappers
{
    public class DatabaseMapper : BaseDataRecordMapper<Database>
    {
        public override Database ToEntity(IDataRecord record)
        {
            return new Database
            {
                Id = record.GetInt32OrDefault(AttributeMetadata.GetColumnIndexByPropertName(nameof(Database.Id))),
                DatabaseCode =
                    record.GetStringOrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(Database.DatabaseCode))),
                DatasetsCount =
                    record.GetInt32OrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(Database.DatasetsCount))),
                Description =
                    record.GetStringOrDefault(AttributeMetadata.GetColumnIndexByPropertName(nameof(Database.Description))),
                Downloads = record.GetInt64(AttributeMetadata.GetColumnIndexByPropertName(nameof(Database.Downloads))),
                Image = record.GetStringOrDefault(AttributeMetadata.GetColumnIndexByPropertName(nameof(Database.Image))),
                Name = record.GetStringOrDefault(AttributeMetadata.GetColumnIndexByPropertName(nameof(Database.Name))),
                Premium = record.GetBoolean(AttributeMetadata.GetColumnIndexByPropertName(nameof(Database.Premium)))
            };
        }
    }
}