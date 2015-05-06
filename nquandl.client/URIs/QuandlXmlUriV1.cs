using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;

namespace NQuandl.Client.URIs
{
    public class QuandlXmlUriV1 : QuandlVersion1Uri
    {
        public QuandlXmlUriV1(string quandlCode, RequestParameterOptions options = null)
            : base(quandlCode, ResponseFormat.XML, options)
        {
        }
    }
}