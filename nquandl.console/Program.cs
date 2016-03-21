using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Util;
using NQuandl.Domain.Quandl.Entities;
using NQuandl.Domain.Quandl.Queries;
using NQuandl.Domain.Quandl.Responses;
using NQuandl.Services.Logger;
using NQuandl.SimpleClient;

namespace nquandl.console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var taskList = new List<Worker>();
            for (var i = 0; i < 10000; i++)
            {
                new Worker().DoWork();
            }

          //  Parallel.ForEach(taskList, x => x.DoWork());
           
            //Task.WaitAll(taskList.ToArray());
            NonBlockingConsole.WriteLine("Done");
            Console.ReadLine();
        }


        private static void GetDatasetBy(string databaseCode, string datasetCode)
        {
            var result = new DatasetBy(databaseCode, datasetCode).Execute().Result;

            foreach (var keyValues in result.dataset.ToKeyValuePairs())
            {
                Console.WriteLine("key: " + keyValues.Key + " value: " + keyValues.Value);
                Console.WriteLine();
            }
        }

        private static void GetDatasetByEntityFredGdp()
        {
            var result = new DatasetByEntity<FredGdp>().Execute();

            foreach (var fredGdp in result.Result.Entities)
            {
                Console.WriteLine(fredGdp.Date);
                Console.WriteLine(fredGdp.Value);
            }
        }


        private static void GetDatabaseMetadataBy()
        {
            var result = new DatabaseMetadataBy("YC").Execute();

            Console.WriteLine(result.Result.database.description);
        }

        private static void GetDatabaseDatasetList()
        {
            var result = new DatabaseDatasetListBy("YC").Execute();

            foreach (var databaseDatasetCsvRow in result.Result.Datasets)
            {
                Console.WriteLine(databaseDatasetCsvRow.DatasetCode);
                Console.WriteLine(databaseDatasetCsvRow.DatasetDescription);
            }
        }

        private static void GetDatabaseSearchBy()
        {
            var result2 = new DatabaseSearchBy
            {
                Query = @"stock+price".ToLowerInvariant()
            }.Execute();

            foreach (var searchDatabase in result2.Result.databases)
            {
                Console.WriteLine(searchDatabase.name);
                Console.WriteLine(searchDatabase.description);
            }
        }

        private static void GetDatabaseListBy()
        {
            var result3 = new DatabaseListBy().Execute();

            foreach (var databasese in result3.Result.databases)
            {
                Console.WriteLine(databasese.database_code);
                Console.WriteLine(databasese.description);
            }
        }
    }

    public class Worker
    {
        public Task DoWork()
        {
            
            return Task.FromResult(Work());

        }

        public async Task Work()
        {
            var databases = new List<DatabaseList>();
            var query = new DatabaseMetadataBy("YC");
             var response = await query.Execute();
             NonBlockingConsole.WriteLine("Done: " + response.database.name);
        }
    }
}