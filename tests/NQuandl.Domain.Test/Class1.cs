using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

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
    }
}
