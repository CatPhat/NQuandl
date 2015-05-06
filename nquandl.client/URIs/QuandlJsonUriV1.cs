using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;

namespace NQuandl.Client.URIs
{
    public class QuandlJsonUriV1 : QuandlVersion1Uri
    {
        public QuandlJsonUriV1(string quandlCode, OptionalRequestParameters optional = null)
            : base(quandlCode, ResponseFormat.JSON, optional)
        {
        }
    }
}