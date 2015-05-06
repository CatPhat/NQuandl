using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;

namespace NQuandl.Client.URIs
{
    public class QuandlCsvUriV1 : QuandlVersion1Uri
    {
        public QuandlCsvUriV1(string quandlCode, OptionalRequestParameters optional = null)
            : base(quandlCode, ResponseFormat.CSV, optional)
        {
        }
    }
}