using NQuandl.Client.Interfaces;
using NQuandl.Client.URIs;

namespace NQuandl.TestConsole
{
    public class TestRequest2 : IQuandlRequest
    {
        public IContainUri Uri
        {
            get { return new TestRequest2Uri(); }
        }
    }
}