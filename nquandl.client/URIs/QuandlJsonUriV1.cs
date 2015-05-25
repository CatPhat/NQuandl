using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;

namespace NQuandl.Client.URIs
{
    public class QuandlJsonUriV1 : QuandlUriV1
    {
        public QuandlJsonUriV1(string quandlCode, QueryParametersV1 options = null)
            : base(quandlCode, ResponseFormat.JSON, options)
        {
        }
    }
}