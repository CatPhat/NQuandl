//using System.Data;
//using NQuandl.Npgsql.Api;
//using NQuandl.Npgsql.Domain.Entities;
//using NQuandl.Npgsql.Services.Extensions;

//namespace NQuandl.Npgsql.Services.Mappers
//{
//    public class DatabaseStatusMapper : BaseDataRecordMapper<DatabaseStatus>
//    {
//        public override DatabaseStatus ToEntity(IDataRecord record)
//        {
//            return new DatabaseStatus
//            {
//                Id = record.GetInt32(Metadata.GetColumnIndexByPropertName(nameof(DatabaseStatus.Id))),
//                DatabaseId =
//                    record.GetInt32(
//                        Metadata.GetColumnIndexByPropertName(nameof(DatabaseStatus.DatabaseId))),
//                DatabaseName =
//                    record.GetStringOrDefault(
//                        Metadata.GetColumnIndexByPropertName(nameof(DatabaseStatus.DatabaseName))),
//                DatasetsAvailable =
//                    record.GetInt32(
//                        Metadata.GetColumnIndexByPropertName(nameof(DatabaseStatus.DatasetsAvailable))),
//                DatasetsDownloaded =
//                    record.GetInt32(
//                        Metadata.GetColumnIndexByPropertName(nameof(DatabaseStatus.DatasetsDownloaded))),
//                DatasetLastChecked =
//                    record.GetDateTime(
//                        Metadata.GetColumnIndexByPropertName(nameof(DatabaseStatus.DatasetLastChecked))),
//                DatabaseMarkedForDownload =
//                    record.GetBoolean(
//                        Metadata.GetColumnIndexByPropertName(nameof(DatabaseStatus.DatabaseMarkedForDownload)))
//            };
//        }
//    }
//}