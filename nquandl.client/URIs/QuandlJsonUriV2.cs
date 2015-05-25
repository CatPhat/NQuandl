using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;

namespace NQuandl.Client.URIs
{
    public class QuandlJsonUriV2 : QuandlUriV2
    {
        public QuandlJsonUriV2(QueryParametersV2 queryParameters)
            : base(ResponseFormat.JSON, queryParameters)
        {
        }
    }
}