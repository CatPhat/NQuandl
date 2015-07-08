using System.Threading.Tasks;
using NQuandl.Client.Entities.Base;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;
using NQuandl.Client.Responses;

namespace NQuandl.Client
{
    public class QuandlJsonService : QuandlService, IQuandlJsonService
    {
        public QuandlJsonService(string baseUrl)
            : base(baseUrl)
        {
        }

       
       
    }
}