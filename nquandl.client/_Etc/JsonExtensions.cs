using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NQuandl.Client
{
    public static class JsonExtensions
    {
        public static T DeserializeToObject<T>(this string jsonResponse) where T : class 
        {
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }
    }
}
