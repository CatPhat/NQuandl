using System;
using System.Net;

namespace NQuandl.Client
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class HttpClient : WebClient
    {
        public int? Timeout { get; set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request != null && Timeout.HasValue) request.Timeout = Timeout.Value;
            return request;
        }
    }
}
