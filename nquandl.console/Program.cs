using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Client.CompositionRoot;
using NQuandl.Client.Domain.Queries;

namespace nquandl.console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Bootstrapper.Bootstrap();
            var queries = Bootstrapper.GetQueryProcessor();
            var result = queries.Execute(new DatabaseListBy()).Result;

            foreach (var databaseDatasetCsvRow in result.databases)
            {
                Console.WriteLine(databaseDatasetCsvRow.database_code);
            }
            Console.ReadLine();

        }
    }
}
