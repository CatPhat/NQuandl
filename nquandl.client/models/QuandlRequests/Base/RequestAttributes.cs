using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client
{
    internal sealed class RequestAttributes : Attribute
    {
        public string DatabaseCode { get; set; }
        public string TableCode { get; set; }
    }
}
