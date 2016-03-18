using System.Collections.Generic;
using System.IO;

namespace NQuandl.Domain.Quandl.Responses
{
    public class HttpClientResponse
    {
        public Stream ContentStream { get; set; }
        public Dictionary<string, IEnumerable<string>> ResponseHeaders { get; set; }
        public string StatusCode { get; set; }
        public bool IsStatusSuccessCode { get; set; }
    }
}