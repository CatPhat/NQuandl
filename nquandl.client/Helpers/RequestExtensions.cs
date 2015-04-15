using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client.Helpers
{
    public static class RequestExtensions
    {
        public static T GetResponseType<T>(this BaseQuandlRequest<T> request) where T : QuandlResponse
        {
            return (T)Activator.CreateInstance(typeof (T));
        }
    }
}
