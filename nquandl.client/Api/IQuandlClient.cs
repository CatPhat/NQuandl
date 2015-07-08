using System.Threading.Tasks;
using NQuandl.Client.Domain;

namespace NQuandl.Client.Api
{
    public interface IQuandlClient
    {
        Task<string> GetAsync(QuandlClientRequestParametersV1 requestParameters);
    }
}