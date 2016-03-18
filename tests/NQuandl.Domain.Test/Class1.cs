using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using NQuandl.Domain.Quandl.Queries;
using NQuandl.Domain.Test.Mock;
using Xunit;


namespace NQuandl.Domain.Test
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Class1
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

        //[Fact]
        //public void DatabaseDatasetListByTest()
        //{
        //    //System.Diagnostics.Debugger.Launch();
        //    var query = new DatabaseDatasetListBy("UN");
        //    var handler = new HandleDatabaseDatasetListBy(new MockDatabaseDatasetListByQuandlClient(), new MockMapCsvStreamMapper());
        //    var result = handler.Handle(query).Result;
        //    Assert.Equal(1, 0);

        //}
    }
}
