using System.Threading.Tasks;

namespace NQuandl.Client._OLD.Interfaces.old
{
    public interface IConsumeHttp
    {
        Task<string> DownloadStringAsync(string url, int? timeout = null, int retries = 0);
    }
}