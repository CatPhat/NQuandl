using System;
using NQuandl.Client.CompositionRoot;
using NQuandl.Client.Domain.Queries;

namespace nquandl.console
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var result = new DatabaseListBy().Execute();
            var result2 = new DatabaseListBy().Execute();
            var result3 = new DatabaseListBy().Execute();
            var result4 = new DatabaseListBy().Execute();

            foreach (var databaseDatasetCsvRow in result.Result.databases)
            {
               Console.WriteLine("result 1");
            }

           

            foreach (var databaseDatasetCsvRow in result2.Result.databases)
            {
               Console.WriteLine("result 2");
            }

            foreach (var databaseDatasetCsvRow in result3.Result.databases)
            {
               Console.WriteLine("result 3");
            }

            foreach (var databaseDatasetCsvRow in result4.Result.databases)
            {
               Console.WriteLine("result 4");
            }
            Console.ReadLine();
        }
    }
}