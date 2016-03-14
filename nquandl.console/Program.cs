using System;
using NQuandl.Client.CompositionRoot;
using NQuandl.Domain.Queries;

namespace nquandl.console
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var result = new DatabaseListBy().Execute();
           

            foreach (var databaseDatasetCsvRow in result.Result.databases)
            {
                Console.WriteLine(databaseDatasetCsvRow.database_code);
                Console.WriteLine(databaseDatasetCsvRow.description);
            }

           
            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}