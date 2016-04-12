using System.Threading.Tasks;

namespace NQuandl.Client.Services.Logger
{
    public interface ILogger
    {

        Task AddInboundRequest(InboundRequestLogEntry entry);
        Task AddCompletedRequest(CompletedRequestLogEntry entry);
        void Write(string logMessage);
    
    }
}