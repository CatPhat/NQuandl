using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client
{
    public interface IConsumeHttp
    {
        Task<string> DownloadStringAsync(string url, int? timeout = null, int retries = 0);
    }
}
