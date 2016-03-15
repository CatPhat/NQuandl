using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using NQuandl.Api;
using NQuandl.Domain.RequestParameters;
using NQuandl.Domain.Responses;

namespace NQuandl.Domain.Test.Mock
{
    public class MockDatabaseDatasetListByQuandlClient : IQuandlClient
    {
        public Task<HttpResponseMessage> GetFullResponseAsync(QuandlClientRequestParameters parameters)
        {
            var stream = File.OpenRead(
                @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\YC-datasets-codes.zip");
            var response = new HttpResponseMessage();
            response.Content.CopyToAsync(stream);
            return Task.FromResult(response);
        }
    }

    public class MockMapCsvStreamMapper : IMapCsvStream
    {
        public Task<IEnumerable<DatabaseDatasetCsvRow>> MapToDataset(StreamReader stream)
        {
            throw new System.NotImplementedException();
        }
    }
}