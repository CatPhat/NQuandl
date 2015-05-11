using NQuandl.Client.Interfaces;
using NQuandl.Client.Responses;
using NQuandl.Client.URIs;

namespace NQuandl.Client.Requests
{
    public class DeserializeMetadataRequestV2 : IQuandlJsonRequest<JsonResponseV2>
    {
        public readonly ForcedRequestOptionsV2 _options;

        public DeserializeMetadataRequestV2(ForcedRequestOptionsV2 options)
        {
            _options = options;
        }

        public IContainUri Uri
        {
            get { return new QuandlJsonUriV2(_options); }
        }
    }
}