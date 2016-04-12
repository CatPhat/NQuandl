using NQuandl.Domain.Requests;
using Xunit;

namespace NQuandl.Domain.Test
{
    public class RequestDatasetbyTests
    {
        [Fact]
        public void PassingTest()
        {
            const int sum = 2 + 2;
            Assert.Equal(4, sum);
        }

        [Fact]
        public void ShouldFail()
        {
            const int sum = 2 + 2;
            Assert.Equal(0, sum);
        }


        [Fact]
        public void RequestDatasetBy_Uri_Should_Be_Valid_By_QuandlCode()
        {
            const string databaseCode = "WIKI";
            const string datasetCode = "AAPL";
            var request = new RequestDatasetBy(databaseCode, datasetCode);

            Assert.Equal(request.ToUri(), $"api/v3/datasets/{databaseCode}/{datasetCode}/data.json");
        }

        
    }
}