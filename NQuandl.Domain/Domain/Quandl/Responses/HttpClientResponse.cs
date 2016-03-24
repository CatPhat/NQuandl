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
        public QuandlErrorResponse QuandlErrorResponse { get; set; }
    }


    public class QuandlErrorResponse
    {
        public Quandl_Error quandl_error { get; set; }
    }

    public class Quandl_Error
    {
        public string code { get; set; }
        public string message { get; set; }
    }

}