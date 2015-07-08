using NQuandl.Client.Api.Helpers;
using NQuandl.Client._OLD.Requests;

namespace NQuandl.Client._OLD.URIs
{
    public class QuandlCsvUriV1V1 : QuandlUriV1
    {
        public QuandlCsvUriV1V1(string quandlCode, QueryParametersV1 options = null)
            : base(quandlCode, ResponseFormat.CSV, options)
        {
        }
    }
}