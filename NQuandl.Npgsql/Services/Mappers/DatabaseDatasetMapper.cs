//using System.Data;
//using NQuandl.Npgsql.Api;
//using NQuandl.Npgsql.Domain.Entities;
//using NQuandl.Npgsql.Services.Extensions;

//namespace NQuandl.Npgsql.Services.Mappers
//{
//    public class DatabaseDatasetMapper : BaseDataRecordMapper<DatabaseDataset>
//    {
//        public override DatabaseDataset ToEntity(IDataRecord record)
//        {
//            return new DatabaseDataset
//            {
//                Id = record.GetInt32(Metadata.GetColumnIndexByPropertName(nameof(DatabaseDataset.Id))),
//                DatabaseCode =
//                    record.GetStringOrDefault(
//                        Metadata.GetColumnIndexByPropertName(nameof(DatabaseDataset.DatabaseCode))),
//                DatasetCode =
//                    record.GetStringOrDefault(
//                        Metadata.GetColumnIndexByPropertName(nameof(DatabaseDataset.DatasetCode))),
//                Description =
//                    record.GetStringOrDefault(
//                        Metadata.GetColumnIndexByPropertName(nameof(DatabaseDataset.Description)))
//            };
//        }
//    }
//}