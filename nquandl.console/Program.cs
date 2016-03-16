using System;
using NQuandl.Domain.Quandl.Entities;
using NQuandl.Domain.Quandl.Queries;
using NQuandl.SimpleClient;

namespace nquandl.console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GetDatasetByFredGdp();
            GetDatasetByFredGdp();
            GetDatasetByFredGdp();
            GetDatasetByFredGdp();


            Console.WriteLine("done");
            Console.ReadLine();
        }

        private static void GetDatasetByFredGdp()
        {
            var result = new DatasetBy<FredGdp>().Execute();

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
}