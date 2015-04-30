using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using NQuandl.Client.Models;

namespace NQuandl.Client
{
    public class TestRequest : IQuandlRequest<TestResponse>
    {
        public string QueryCode
        {
            get { throw new NotImplementedException(); }
        }
        
        public string Url
        {
            get { return "http://localhost:49832/api/testapi/"; }
        }
    }


    public class TestResponse : QuandlResponse
    {
        public TestResponse(QuandlCode quandlCode) : base(quandlCode)
        {
        }

        public string RequestType { get; set; }
        public int UniqueId { get; set; }
        public int RequestCount { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int Milliseconds { get; set; }
    }





}



