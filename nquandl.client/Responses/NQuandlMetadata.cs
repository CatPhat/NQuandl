using System;
using System.Net.Http;

namespace NQuandl.Client.Responses
{
    public class NQuandlMetadata
    {
        public HttpResponseMessage HttpResponseMessage { get; set; }
        public DateTime RequestStartTime { get; set; }
        public DateTime RequestEndTime { get; set; }

        public TimeSpan RequestTimeSpan
        {
            get { return RequestStartTime - RequestEndTime; }
        }
    }
}