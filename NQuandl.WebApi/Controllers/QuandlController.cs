using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using NQuandl.Domain.Quandl.Queries;
using NQuandl.Domain.Quandl.Responses;
using NQuandl.SimpleClient;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NQuandl.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class QuandlController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            
            return new string[] { "value1", "value2" };
        }

        // GET: api/database_list/startIndex/endIndex/orderBy
        [Route("database_list/{startIndex:int}/{endIndex:int}/{orderBy}")]
        public async Task<JsonResult> Get(int startIndex, int endIndex, string orderBy)
        {
            var result = await new GetAllDatabaseListsBy().Execute();

            switch (orderBy.ToLowerInvariant())
            {
                case "count":
                    result = result.OrderByDescending(x => x.datasets_count);
                    break;

                default:
                    result = result.OrderBy(x => x.name);
                    break;
            }
            
            var results = new List<Databases>();
            for (var i = startIndex; i <= endIndex; i++)
            {
                var db = result.ElementAtOrDefault(i);
                if (db != null)
                {
                    results.Add(db);
                }
              
            }
            return new JsonResult(results);
        }


        // GET: api/datasets_list/databaseCode/startIndex/endIndex/orderBy
        [Route("dataset_list/{databaseCode}/{startIndex:int}/{endIndex:int}/{orderBy}")]
        public async Task<JsonResult> Get(string databaseCode, int startIndex, int endIndex, string orderBy)
        {
            var result = await new DatabaseDatasetListBy(databaseCode).Execute();

            var results = new List<DatabaseDatasetCsvRow>();

            for (int i = startIndex; i <= endIndex; i++)
            {
                var dataset = result.Datasets.ElementAtOrDefault(i);
                if (dataset != null)
                {
                    results.Add(dataset);
                }
            }

            return new JsonResult(results);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
