//using System.Data;
//using NQuandl.Npgsql.Api;
//using NQuandl.Npgsql.Domain.Entities;
//using NQuandl.Npgsql.Services.Extensions;

//namespace NQuandl.Npgsql.Services.Mappers
//{
//    public class DatabaseMapper : BaseDataRecordMapper<Database>
//    {
//        public override Database ToEntity(IDataRecord record)
//        {
//            return new Database
//            {
//                Id = record.GetInt32(Metadata.GetColumnIndexByPropertName(nameof(Database.Id))),
//                DatabaseCode =
//                    record.GetStringOrDefault(
//                        Metadata.GetColumnIndexByPropertName(nameof(Database.DatabaseCode))),
//                DatasetsCount =
//                    record.GetInt32(
//                        Metadata.GetColumnIndexByPropertName(nameof(Database.DatasetsCount))),
//                Description =
//                    record.GetStringOrDefault(Metadata.GetColumnIndexByPropertName(nameof(Database.Description))),
//                Downloads = record.GetInt64(Metadata.GetColumnIndexByPropertName(nameof(Database.Downloads))),
//                Image = record.GetStringOrDefault(Metadata.GetColumnIndexByPropertName(nameof(Database.Image))),
//                Name = record.GetStringOrDefault(Metadata.GetColumnIndexByPropertName(nameof(Database.Name))),
//                Premium = record.GetBoolean(Metadata.GetColumnIndexByPropertName(nameof(Database.Premium)))
//            };
//        }
//    }
//}