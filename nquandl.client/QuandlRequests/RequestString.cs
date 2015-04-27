using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client.Models.QuandlRequests
{
    public class RequestString : BaseQuandlRequestV1<RequestStringResponse>
    {
        public RequestString(QuandlCode quandlCode, OptionalRequestParameters options = null)
            : base(quandlCode, options)
        {
        }
    }

    public class RequestStringResponse : QuandlResponse
    {
        public string String { get; set; }
    }
}
