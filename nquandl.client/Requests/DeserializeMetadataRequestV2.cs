using NQuandl.Client.Interfaces;
using NQuandl.Client.Responses;
using NQuandl.Client.URIs;

namespace NQuandl.Client.Requests
{
    public class DeserializeMetadataRequestV2 : IQuandlJsonRequest<JsonResponseV2>
    {
        private  readonly QueryParametersV2 _options;

        public DeserializeMetadataRequestV2(QueryParametersV2 options)
        {
            _options = options;
        }

        public IQuandlUri Uri
        {
            get { return new QuandlJsonUriV2(_options); }
        }
    }
}