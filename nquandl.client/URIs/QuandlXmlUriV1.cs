using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;

namespace NQuandl.Client.URIs
{
    public class QuandlXmlUriV1 : QuandlVersion1Uri
    {
        public QuandlXmlUriV1(string quandlCode, OptionalRequestParameters optional = null)
            : base(quandlCode, ResponseFormat.XML, optional)
        {
        }
    }
}