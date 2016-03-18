using System.Collections.Generic;

namespace NQuandl.Domain.Quandl.Responses
{
    public class QuandlClientResponseInfo
    {
        public string StatusCode { get; set; }
        public bool IsStatusSuccessCode { get; set; }
        public Dictionary<string, IEnumerable<string>> ResponseHeaders { get; set; }
    }
}