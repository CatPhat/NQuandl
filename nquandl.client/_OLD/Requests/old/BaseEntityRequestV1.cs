using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client.Requests
{
    public abstract class BaseEntityRequestV1
    {
        public QueryParametersV1 QueryParametersV1 { get; set; }
    }
}
