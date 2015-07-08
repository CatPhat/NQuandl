using NQuandl.Client._OLD.Interfaces.old;

namespace NQuandl.Client._OLD
{
    public class QuandlJsonService : QuandlService, IQuandlJsonService
    {
        public QuandlJsonService(string baseUrl)
            : base(baseUrl)
        {
        }

       
       
    }
}