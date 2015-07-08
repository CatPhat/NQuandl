using NQuandl.Client.Api.Helpers;
using NQuandl.Client._OLD.Requests;

namespace NQuandl.Client._OLD.URIs
{
    public class QuandlJsonUriV2 : QuandlUriV2
    {
        public QuandlJsonUriV2(QueryParametersV2 queryParameters)
            : base(ResponseFormat.JSON, queryParameters)
        {
        }
    }
}