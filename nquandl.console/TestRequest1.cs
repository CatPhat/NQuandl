using NQuandl.Client.Interfaces;
using NQuandl.Client.URIs;

namespace NQuandl.TestConsole
{
    public class TestRequest1 : IQuandlRequest
    {
        public IContainUri Uri
        {
            get { return new TestRequest1Uri(); }
        }
    }
}