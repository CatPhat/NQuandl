using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;

namespace NQuandl.Client.URIs
{
    public class QuandlXmlUriV1V1 : QuandlUriV1
    {
        public QuandlXmlUriV1V1(string quandlCode, RequestOptionsV1 options = null)
            : base(quandlCode, ResponseFormat.XML, options)
        {
        }
    }
}