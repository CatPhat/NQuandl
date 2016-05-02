//using System.Data;
//using Newtonsoft.Json.Linq;
//using NQuandl.Npgsql.Api;
//using NQuandl.Npgsql.Domain.Entities;
//using NQuandl.Npgsql.Services.Extensions;

//namespace NQuandl.Npgsql.Services.Mappers
//{
//    public class DatasetMapper : BaseDataRecordMapper<Dataset>
//    {
//        public override Dataset ToEntity(IDataRecord record)
//        {
//            return new Dataset
//            {
//                Id = record.GetInt32(Metadata.GetColumnIndexByPropertName(nameof(Dataset.Id))),
//                Code = record.GetStringOrDefault(Metadata.GetColumnIndexByPropertName(nameof(Dataset.Code))),
//                Data = record[(Metadata.GetColumnIndexByPropertName(nameof(Dataset.Data)))] as JArray,
//                DatabaseCode =
//                    record.GetStringOrDefault(Metadata.GetColumnIndexByPropertName(nameof(Dataset.DatabaseCode))),
//                DatabaseId = record.GetInt32(Metadata.GetColumnIndexByPropertName(nameof(Dataset.DatabaseId))),
//                Description =
//                    record.GetStringOrDefault(Metadata.GetColumnIndexByPropertName(nameof(Dataset.Description))),
//                EndDate = record.GetDateTime(Metadata.GetColumnIndexByPropertName(nameof(Dataset.EndDate))),
//                Frequency =
//                    record.GetStringOrDefault(Metadata.GetColumnIndexByPropertName(nameof(Dataset.Frequency))),
//                Name = record.GetStringOrDefault(Metadata.GetColumnIndexByPropertName(nameof(Dataset.Name))),
//                RefreshedAt =
//                    record.GetDateTime(Metadata.GetColumnIndexByPropertName(nameof(Dataset.RefreshedAt))),
//                StartDate = record.GetDateTime(Metadata.GetColumnIndexByPropertName(nameof(Dataset.StartDate)))
//            };
//        }
//    }
//}