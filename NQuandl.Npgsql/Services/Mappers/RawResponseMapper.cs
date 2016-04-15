using System.Data;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Services.Mappers
{
    public class RawResponseMapper : BaseDataRecordMapper<RawResponse>
    {
        public override RawResponse ToEntity(IDataRecord record)
        {
            return new RawResponse
            {
                Id = record.GetInt32(AttributeMetadata.GetColumnIndexByPropertName(nameof(RawResponse.Id))),
                CreationDate =
                    record.GetDateTime(AttributeMetadata.GetColumnIndexByPropertName(nameof(RawResponse.CreationDate))),
                RequestUri =
                    record.GetString(AttributeMetadata.GetColumnIndexByPropertName(nameof(RawResponse.RequestUri))),
                ResponseContent =
                    record.GetString(AttributeMetadata.GetColumnIndexByPropertName(nameof(RawResponse.ResponseContent)))
            };
        }
    }
}