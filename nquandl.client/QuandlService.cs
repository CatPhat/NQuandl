using System;
using System.Threading.Tasks;



namespace NQuandl.Client
{
    public class QuandlService : IQuandlService
    {
        public async Task<T> GetAsync<T>(BaseQuandlRequest<T> request) where T : QuandlResponse
        {
            var response = await GetStringAsync(request);
            return response.DeserializeToObject<T>();
        }

        public async Task<string> GetStringAsync<T>(BaseQuandlRequest<T> request) where T : QuandlResponse
        {
            return await new WebClientHttpConsumer().DownloadStringAsync(request.Url);
        }
       
    }

   


 
   



  

   
}
