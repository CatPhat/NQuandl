namespace NQuandl.Services.Logger
{
    public interface ILogger
    {

        void AddInboundRequest(string request);
        void AddCompletedRequest(string request);
        void Write(string logMessage);
    }
}