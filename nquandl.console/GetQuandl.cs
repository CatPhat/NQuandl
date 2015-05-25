using System;
using NQuandl.Client;
using NQuandl.Client.Entities;
using NQuandl.Client.Requests;

namespace NQuandl.TestConsole
{
    public class GetQuandl
    {
        public void Get()
        {
            var options = new QueryParametersV1();

            var service = new QuandlJsonService("https://quandl.com/api");
            var result = service.GetAsync<FRED_GDP>(options).Result;

            foreach (var entity in result.Entities)
            {
                Console.WriteLine(entity.Date + " | " + entity.Value);
            }
        }
    }
}