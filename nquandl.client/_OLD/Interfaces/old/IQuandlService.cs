using System.Threading.Tasks;

namespace NQuandl.Client._OLD.Interfaces.old
{
    public interface IQuandlService
    {
        Task<string> GetStringAsync(QuandlRequest request);
    }
}