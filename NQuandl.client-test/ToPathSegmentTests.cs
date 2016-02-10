using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NQuandl.Client.Api.Helpers;
using NQuandl.Client.Domain.Entities;
using NQuandl.Client.Domain.Queries;

namespace NQuandl.client_test
{
    [TestClass]
    public class ToPathSegmentTests
    {
        [TestMethod]
        //https://www.quandl.com/api/v3/databases/YC/codes
        public void DatabaseDatasetListBy_ToPathSegmentTest()
        {
            var query = new DatabaseDatasetListBy("YC");
            Assert.AreEqual(query.ToPathSegment(), "v3/databases/YC/codes");
        }

        [TestMethod]
        // https://www.quandl.com/api/v3/databases.json
        public void DatabaseListBy_ToPathSegmentTest()
        {
            var query = new DatabaseListBy();
            Assert.AreEqual(query.ToPathSegment(), "v3/databases.json");
        }

        [TestMethod]
        // https://www.quandl.com/api/v3/databases/WIKI.json
        public void DatabaseMetadataBy_ToPathSegmentTest()
        {
            var query = new DatabaseMetadataBy("WIKI");
            Assert.AreEqual(query.ToPathSegment(), "v3/databases/WIKI.json");
        }

        [TestMethod]
        // https://www.quandl.com/api/v3/databases.json
        public void DatabaseSearchBy_ToPathSegmentTest()
        {
            var query = new DatabaseSearchBy();
            Assert.AreEqual(query.ToPathSegment(), "v3/databases.json");
        }

        [TestMethod]
        // https://www.quandl.com/api/v3/datasets/FRED/GDP.json
        public void DatasetBy_ToPathSegmentTest()
        {
            var query = new DatasetBy<FredGdp>();
            Assert.AreEqual(query.ToPathSegment(), "v3/datasets/FRED/GDP.json");
        }




    }
}
