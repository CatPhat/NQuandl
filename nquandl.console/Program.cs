using System;

using NQuandl.Domain.Queries;
using NQuandl.SimpleClient;

namespace nquandl.console
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var result = new DatabaseDatasetListBy("UN").Execute();
            var result2 = new DatabaseSearchBy().Execute();
           

            foreach (var databaseDatasetCsvRow in result.Result.Datasets)
            {
                Console.WriteLine(databaseDatasetCsvRow.DatasetCode);
                Console.WriteLine(databaseDatasetCsvRow.DatasetDescription);
            }

            foreach (var searchDatabase in result2.Result.databases)
            {
                Console.WriteLine(searchDatabase.name);
                Console.WriteLine(searchDatabase.description);
            }

           
            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}