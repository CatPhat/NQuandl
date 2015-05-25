using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;

namespace NQuandl.Client.URIs
{
    public class QuandlCsvUriV1V1 : QuandlUriV1
    {
        public QuandlCsvUriV1V1(string quandlCode, QueryParametersV1 options = null)
            : base(quandlCode, ResponseFormat.CSV, options)
        {
        }
    }
}