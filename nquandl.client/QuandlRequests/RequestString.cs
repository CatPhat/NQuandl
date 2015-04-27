using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client.Models.QuandlRequests
{
    public class RequestString : BaseQuandlRequestV1<RequestStringResponse>
    {
        private readonly string _queryCode;
        private readonly string _parameters;
        public RequestString(string querycode, string parameters)
        {
            _queryCode = querycode;
            _parameters = parameters;
        }

        public override string QueryCode
        {
            get { return _queryCode; }
        }

        public override string Parameters
        {
            get { return _parameters; }
        }
    }

    public class RequestStringResponse : QuandlResponse
    {
        public string String { get; set; }
    }
}
