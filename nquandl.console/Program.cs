using System;
using NQuandl.Client.CompositionRoot;
using NQuandl.Client.Domain.Queries;

namespace nquandl.console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var result = new DatabaseListBy().Execute().Result;

            foreach (var databaseDatasetCsvRow in result.databases)
            {
                Console.WriteLine(databaseDatasetCsvRow.database_code);
                Console.WriteLine(databaseDatasetCsvRow.description);
            }

            var result2 = new DatabaseListBy().Execute().Result;

            foreach (var databaseDatasetCsvRow in result2.databases)
            {
                Console.WriteLine(databaseDatasetCsvRow.database_code);
                Console.WriteLine(databaseDatasetCsvRow.description);
            }
            Console.ReadLine();
        }
    }
}