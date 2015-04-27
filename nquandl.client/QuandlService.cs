using System;
using System.Threading.Tasks;



namespace NQuandl.Client
{
    public class QuandlService : IQuandlService
    {
        public async Task<T> GetAsync<T>(IQuandlRequest<T> request) where T : QuandlResponse
        {
            var response = await GetStringAsync(request);
            return await response.DeserializeToObjectAsync<T>();
        }

        public async Task<string> GetStringAsync<T>(IQuandlRequest<T> request) where T : QuandlResponse
        {
            return await new WebClientHttpConsumer().DownloadStringAsync(request.Url);
        }
       
    }

   


 
   



  

   
}
