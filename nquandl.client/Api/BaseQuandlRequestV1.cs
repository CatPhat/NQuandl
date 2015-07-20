using NQuandl.Client.Domain.RequestParameters;

namespace NQuandl.Client.Api
{
    public abstract class BaseQuandlRequestV1
    {
        public RequestParametersV1 RequestParameters { get; set; }
    }
}