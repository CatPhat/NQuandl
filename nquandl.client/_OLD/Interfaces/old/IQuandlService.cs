using System.Threading.Tasks;

namespace NQuandl.Client.Interfaces
{
    public interface IQuandlService
    {
        Task<string> GetStringAsync(QuandlRequest request);
    }
}