using System.Threading.Tasks;

namespace NQuandl.Client.Interfaces
{
    public interface IConsumeHttp
    {
        Task<string> DownloadStringAsync(string url, int? timeout = null, int retries = 0);
    }
}
