using System.Data;
using Newtonsoft.Json.Linq;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Services.Mappers
{
    public class DatasetMapper : BaseDataRecordMapper<Dataset>
    {
        public override Dataset ToEntity(IDataRecord record)
        {
            return new Dataset
            {
                Id = record.GetInt32(AttributeMetadata.GetColumnIndexByPropertName(nameof(Dataset.Id))),
                Code = record.GetStringOrDefault(AttributeMetadata.GetColumnIndexByPropertName(nameof(Dataset.Code))),
                Data = record[(AttributeMetadata.GetColumnIndexByPropertName(nameof(Dataset.Data)))] as JArray,
                DatabaseCode =
                    record.GetStringOrDefault(AttributeMetadata.GetColumnIndexByPropertName(nameof(Dataset.DatabaseCode))),
                DatabaseId = record.GetInt32(AttributeMetadata.GetColumnIndexByPropertName(nameof(Dataset.DatabaseId))),
                Description =
                    record.GetStringOrDefault(AttributeMetadata.GetColumnIndexByPropertName(nameof(Dataset.Description))),
                EndDate = record.GetDateTime(AttributeMetadata.GetColumnIndexByPropertName(nameof(Dataset.EndDate))),
                Frequency =
                    record.GetStringOrDefault(AttributeMetadata.GetColumnIndexByPropertName(nameof(Dataset.Frequency))),
                Name = record.GetStringOrDefault(AttributeMetadata.GetColumnIndexByPropertName(nameof(Dataset.Name))),
                RefreshedAt =
                    record.GetDateTime(AttributeMetadata.GetColumnIndexByPropertName(nameof(Dataset.RefreshedAt))),
                StartDate = record.GetDateTime(AttributeMetadata.GetColumnIndexByPropertName(nameof(Dataset.StartDate)))
            };
        }
    }
}