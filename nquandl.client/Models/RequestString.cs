using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client.Models.QuandlRequests
{
    public class RequestString : BaseQuandlRequestV1<RequestStringResponse>
    {
        public RequestString(RequestParameters parameters)
            : base(parameters)
        {
        }

        
    }

    public class RequestStringResponse : QuandlResponse
    {
        public RequestStringResponse(QuandlCode quandlCode) : base(quandlCode)
        {
           
        }

        public string String { get; set; }
    }
}
