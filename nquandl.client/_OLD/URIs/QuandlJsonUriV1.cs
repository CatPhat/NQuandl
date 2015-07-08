using NQuandl.Client.Api.Helpers;
using NQuandl.Client._OLD.Requests;

namespace NQuandl.Client._OLD.URIs
{
    public class QuandlJsonUriV1 : QuandlUriV1
    {
        public QuandlJsonUriV1(string quandlCode, QueryParametersV1 options = null)
            : base(quandlCode, ResponseFormat.JSON, options)
        {
        }
    }
}