//using System.Data;
//using NQuandl.Npgsql.Api;
//using NQuandl.Npgsql.Domain.Entities;
//using NQuandl.Npgsql.Services.Extensions;
//using NQuandl.Npgsql.Services.Helpers;

//namespace NQuandl.Npgsql.Services.Mappers
//{
//    public class RawResponseMapper : BaseDataRecordMapper<RawResponse>
//    {
//        public override RawResponse ToEntity(IDataRecord record)
//        {
//            return new RawResponse
//            {
//                Id = record.GetInt32(Metadata.GetColumnIndexByPropertName(nameof(RawResponse.Id))),
//                CreationDate =
//                    record.GetDateTime(Metadata.GetColumnIndexByPropertName(nameof(RawResponse.CreationDate))),
//                RequestUri =
//                    record.GetStringOrDefault(Metadata.GetColumnIndexByPropertName(nameof(RawResponse.RequestUri))),
//                ResponseContent =
//                    record.GetStringOrDefault(Metadata.GetColumnIndexByPropertName(nameof(RawResponse.ResponseContent)))
//            };
//        }
//    }
//}