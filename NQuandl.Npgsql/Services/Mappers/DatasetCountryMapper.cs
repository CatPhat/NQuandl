//using System.Data;
//using NQuandl.Npgsql.Api;
//using NQuandl.Npgsql.Domain.Entities;
//using NQuandl.Npgsql.Services.Extensions;

//namespace NQuandl.Npgsql.Services.Mappers
//{
//    public class DatasetCountryMapper : BaseDataRecordMapper<DatasetCountry>
//    {
//        public override DatasetCountry ToEntity(IDataRecord record)
//        {
//            return new DatasetCountry
//            {
//                Id = record.GetInt32(Metadata.GetColumnIndexByPropertName(nameof(DatasetCountry.Id))),
//                DatasetId =
//                    record.GetInt32(
//                        Metadata.GetColumnIndexByPropertName(nameof(DatasetCountry.DatasetId))),
//                Iso31661Alpha3 =
//                    record.GetStringOrDefault(
//                        Metadata.GetColumnIndexByPropertName(nameof(DatasetCountry.Iso31661Alpha3))),
//                Association =
//                    record.GetStringOrDefault(
//                        Metadata.GetColumnIndexByPropertName(nameof(DatasetCountry.Association)))
//            };
//        }
//    }
//}