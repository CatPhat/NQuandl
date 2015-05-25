using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NQuandl.Client.Responses;

namespace NQuandl.Client
{
    public class NQuandlClient : QuandlService
    {
        public NQuandlClient(string baseUrl) : base(baseUrl)
        {
        }


    }


    public class NQuandlResponse
    {
      

        public DateTime RequestStartTime;
        public DateTime RequestCompletionTime;

        public TimeSpan RequestDuration
        {
            get { return RequestStartTime - RequestCompletionTime; }
        }

        public void SetRequestStartTime()
        {
            RequestStartTime = DateTime.Now;
        }

        public void SetRequestCompletionTime()
        {
            RequestCompletionTime = DateTime.Now;
        }

        public string RawResponse { get; set; }
    }

    public class NQuandlDeserializedJsonResponse<TResponse> : NQuandlResponse where TResponse : JsonResponse
    {
        private readonly DeserializedJsonResponse<TResponse> _response;
        public NQuandlDeserializedJsonResponse()
        {
         
        }
        public DeserializedJsonResponse<TResponse> Response
        {
            get
            {
                return new DeserializedJsonResponse<TResponse>(RawResponse);
            }
        }
    }
}
