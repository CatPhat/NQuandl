using System;
using System.Collections.Generic;
using NQuandl.Domain.Persistence.Commands;
using NQuandl.Domain.Quandl.Requests;
using NQuandl.Services.Logger;
using NQuandl.Services.PostgresEF7.Models;
using NQuandl.SimpleClient;

namespace nquandl.console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var command = new CreateRawResponse
            {
                Content = "test Content",
                Uri = "testURI"
            };

            var command2 = new CreateRawResponse
            {
                Content = "test content 2",
                Uri = "testUri2"
            };
            command.Execute().Wait();
            command2.Execute().Wait();
   
            NonBlockingConsole.WriteLine("Done");
            Console.ReadLine();
        }
    }


        //    private static void GetDatasetBy(string databaseCode, string datasetCode)
        //    {
        //        var result = new DatasetBy(databaseCode, datasetCode).Execute().Result;

        //        foreach (var keyValues in result.dataset.ToKeyValuePairs())
        //        {
        //            Console.WriteLine("key: " + keyValues.Key + " value: " + keyValues.Value);
        //            Console.WriteLine();
        //        }
        //    }

        //    private static void GetDatasetByEntityFredGdp()
        //    {
        //        var result = new DatasetByEntity<FredGdp>().Execute().Result;
        //        if (result.QuandlClientResponseInfo.IsStatusSuccessCode)
        //        {
        //            foreach (var fredGdp in result.Entities)
        //            {
        //                Console.WriteLine(fredGdp.Date);
        //                Console.WriteLine(fredGdp.Value);
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine(result.QuandlClientResponseInfo.QuandlErrorResponse.quandl_error.message);
        //        }

        //    }


        //    private static void GetDatabaseMetadataBy()
        //    {
        //        var result = new DatabaseMetadataBy("YC").Execute();

        //        Console.WriteLine(result.Result.database.description);
        //    }

        //    private static void GetDatabaseDatasetList()
        //    {
        //        var result = new DatabaseDatasetListBy("YC").Execute();

        //        foreach (var databaseDatasetCsvRow in result.Result.Datasets)
        //        {
        //            Console.WriteLine(databaseDatasetCsvRow.DatasetCode);
        //            Console.WriteLine(databaseDatasetCsvRow.DatasetDescription);
        //        }
        //    }

        //    private static void GetDatabaseSearchBy()
        //    {
        //        var result2 = new DatabaseSearchBy
        //        {
        //            Query = @"stock+price".ToLowerInvariant()
        //        }.Execute();

        //        foreach (var searchDatabase in result2.Result.databases)
        //        {
        //            Console.WriteLine(searchDatabase.name);
        //            Console.WriteLine(searchDatabase.description);
        //        }
        //    }

        //    private static void GetDatabaseListBy()
        //    {
        //        var result3 = new DatabaseListBy { Page = 1 }.Execute();

        //        foreach (var databasese in result3.Result.databases)
        //        {
        //            Console.WriteLine(databasese.database_code);
        //            Console.WriteLine(databasese.description);
        //        }
        //    }
        //}

        //public class Worker
        //{
        //    public Task DoWork()
        //    {
        //        return Task.FromResult(Work());
        //    }

        //    public async Task Work()
        //    {
        //        var databases = new List<DatabaseList>();
        //        var query = new DatabaseMetadataBy("YC");
        //        var response = await query.Execute();
        //        NonBlockingConsole.WriteLine("Done: " + response.database.name);
        //    }
        //}

        //public class DownloadAllDatasetsAndSaveToFile
        //{
        //    public async Task Get()
        //    {
        //        var databaseList1 = new DatabaseListBy {Page = 1};


        //        var client = QueryExtensions.GetQuandlClient();

        //        var response = await client.GetStringAsync(databaseList1.ToQuandlClientRequestParameters());


        //        var databaseList = new List<string> {response.ContentString};
        //        var deserializedResponse = response.ContentString.DeserializeToEntity<DatabaseList>();


        //        for (var i = deserializedResponse.meta.current_page; i <= deserializedResponse.meta.total_pages; i++)
        //        {
        //            var query = new DatabaseListBy {Page = i};
        //            var queryResponse = await client.GetStringAsync(query.ToQuandlClientRequestParameters());
        //            databaseList.Add(queryResponse.ContentString);

        //            var databaseListFileName = GetFileNameFromUri(query.ToQuandlClientRequestParameters().ToUri());
        //            var databaseListFileFullPath = GetFullFilePathFromFileName(databaseListFileName, "databaseList");


        //            WriteToFile(queryResponse.ContentString, databaseListFileFullPath);


        //        }


        //        var dbLists = new List<Databases>();
        //        foreach (var dbString in databaseList)
        //        {

        //            dbLists.AddRange(dbString.DeserializeToEntity<DatabaseList>().databases.ToList());
        //        }

        //        foreach (var databasese in dbLists.OrderBy(x => x.datasets_count).Where(y => y.premium == false && y.database_code != "BOE"))
        //        {
        //            NonBlockingConsole.WriteLine("Database Name: " + databasese.name);
        //            NonBlockingConsole.WriteLine("Dataset Count: " + databasese.datasets_count);

        //            var databaseDatasetList = await new DatabaseDatasetListBy(databasese.database_code).Execute();

        //            var datasetQueries = new List<DatasetBy>();
        //            foreach (var databaseDatasetCsvRow in databaseDatasetList.Datasets.Take(500))
        //            {
        //                var databaseDatasetQuery = new DatasetBy(databaseDatasetCsvRow.DatabaseCode,
        //                    databaseDatasetCsvRow.DatasetCode);

        //                var fileName =
        //                    GetFileNameFromUri(databaseDatasetQuery.ToQuandlClientRequestParameters().ToUri());

        //                var fullFilePath = GetFullFilePathFromFileName(fileName, "json");
        //                if (!CheckIfFileExists(fullFilePath))
        //                {
        //                    datasetQueries.Add(databaseDatasetQuery);
        //                }
        //                else
        //                {
        //                    NonBlockingConsole.WriteLine("File Exists: " + fileName);
        //                }
        //            }
        //            foreach (var datasetQuery in datasetQueries)
        //            {
        //                var fileName =
        //                       GetFileNameFromUri(datasetQuery.ToQuandlClientRequestParameters().ToUri());

        //                var fullFilePath = GetFullFilePathFromFileName(fileName, "json");
        //                var datasetStringResponse =
        //                           await client.GetStringAsync(datasetQuery.ToQuandlClientRequestParameters());
        //                await Task.Run(() => WriteToFile(datasetStringResponse.ContentString, fullFilePath));
        //            }
        //        }


        //    }


        //}
    
}