using System;
using System.Threading.Tasks;

namespace NQuandl.Services.Logger
{
    public interface ILogger
    {

        Task AddInboundRequest(InboundRequestLogEntry entry);
        Task AddCompletedRequest(CompletedRequestLogEntry entry);
        void Write(string logMessage);
    
    }
}