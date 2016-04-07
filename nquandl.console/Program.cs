using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Api.Quandl.Helpers;
using NQuandl.Domain.Persistence;
using NQuandl.Domain.Persistence.Commands;
using NQuandl.Domain.Persistence.Entities;
using NQuandl.Domain.Persistence.Queries;
using NQuandl.Domain.Quandl.Requests;
using NQuandl.Domain.Quandl.Responses;
using NQuandl.Services.Logger;
using NQuandl.Services.PostgresEF7.Models;
using NQuandl.SimpleClient;

namespace nquandl.console
{
    public class Program
    {
        public static void Main(string[] args)
        {




            new GetAllDatabaseDatasetListEntriesFromFiles().Get().Wait();


            NonBlockingConsole.WriteLine("Done");
            Console.ReadLine();
        }

    
   
    }


    public class GetAllDatabaseDatasetListEntriesFromFiles
    {
        public async Task Get()
        {
            var dbList1 = await new RequestDatabaseListBy {Page = 1}.ExecuteRequest();
            for (int page = 1; page <= dbList1.Metadata.total_pages.Value; page++)
            {
                var dbList = await new RequestDatabaseListBy {Page = page}.ExecuteRequest();

                foreach (var databaseListDatabase in dbList.Databases.OrderByDescending(x => x.DatabaseCode))
                {
                    NonBlockingConsole.WriteLine("Starting Database Code: " + databaseListDatabase.DatabaseCode);
                    var datasetsList = await new RequestDatabaseDatasetListBy(databaseListDatabase.DatabaseCode).ExecuteRequest();
                    var rows = GetCsvRows(datasetsList.Datasets);

                    var database = await new DatabaseBy(databaseListDatabase.DatabaseCode).ExecuteQuery();
                    var datasetsCount =
                        await new DatabaseDatasetListEntryCountBy(databaseListDatabase.DatabaseCode).ExecuteQuery();
             
                    if (datasetsCount != database.DatasetsCount && datasetsCount <= database.DatasetsCount)
                    {
                        NonBlockingConsole.WriteLine("Count does not match: " + databaseListDatabase.DatabaseCode);
                        NonBlockingConsole.WriteLine("Count in Database Table: " + database.DatasetsCount);
                        NonBlockingConsole.WriteLine("Count in DbListEntries: " + datasetsCount);
                        Parallel.ForEach(rows, new ParallelOptions {MaxDegreeOfParallelism = 3}, async row =>
                        {
                            var entity = await new DatabaseDatasetListEntryBy(row.QuandlCode).ExecuteQuery();

                            if (entity == null)
                            {
                                var createEntry = new CreateDatabaseDatasetListEntry(new DatabaseDatasetListEntry
                                {
                                    DatabaseCode = row.DatabaseCode,
                                    DatasetCode = row.DatasetCode,
                                    Description = row.DatasetDescription,
                                    QuandlCode = row.QuandlCode
                                });
                                //NonBlockingConsole.WriteLine("Adding: " + createEntry.Entry.QuandlCode + " " + new DateTime());
                                await createEntry.ExecuteCommand();
                            }

                        });
                    }
                    else
                    {
                        NonBlockingConsole.WriteLine("Count Matches: " + databaseListDatabase.DatabaseCode);
                    }

            
                    NonBlockingConsole.WriteLine("Finished Database Code: " + databaseListDatabase.DatabaseCode);
                }
            }


        }


        public async Task GetMCX()
        {
            var dbList = await new RequestDatabaseDatasetListBy("MCX").ExecuteRequest();
            var rows = GetCsvRows(dbList.Datasets);
            foreach (var csvDatabaseDataset in rows)
            {
                Console.WriteLine("Quandlcode: " + csvDatabaseDataset.QuandlCode);
                Console.WriteLine("Database Code: " + csvDatabaseDataset.DatabaseCode);
                Console.WriteLine("Dataset Code: " + csvDatabaseDataset.DatasetCode);
                Console.WriteLine("Description: " + csvDatabaseDataset.DatasetDescription);
            }
        }

        private static IEnumerable<CsvDatabaseDataset> GetCsvRows(StreamReader stream)
        {

            string line;
            while ((line = stream.ReadLine()) != null)
            {
                var columnZeroIndex = line.IndexOf(',');

                var quandlCode = line.Substring(0, columnZeroIndex);
                var description = line.Substring(columnZeroIndex + 1);
                var splitQuandlCode = quandlCode.Split('/');

                var dataset = new CsvDatabaseDataset
                {
                    DatabaseCode = splitQuandlCode[0],
                    DatasetCode = splitQuandlCode[1],
                    QuandlCode = quandlCode,
                    DatasetDescription = description
                };
                yield return dataset;
            }



        }
    }

    public class GetAllDatasetsFromRawResponses
    {
        public async Task Get()
        {
            var entities = QueryExtensions.GetReadEntities();
            var rawResponses = entities.Query<RawResponse>();

            foreach (var rawResponse in rawResponses)
            {
                rawResponse.ResponseContent.DeserializeToEntity<JsonResultDatasetDataAndMetadata>();
            }
        }
    }
    

    public class GetAllDatasetsFromFiles
    {
        public async Task Get()
        {

            var d = new DirectoryInfo(@"C:\Users\USER9\Documents\quandl_data\output\json");
            var files = d.GetFiles("*.json");
            var options = new ParallelOptions { MaxDegreeOfParallelism = 4 };
       
           
            Parallel.ForEach(files,options, async file =>
            {
                string fileContent;
                using (StreamReader sr = File.OpenText(file.FullName))
                {
                    fileContent = await sr.ReadToEndAsync();
                }

                var createRawResponse = new CreateRawResponse
                {

                    Uri = "UnknownDataset",
                    Content = fileContent
                };

                await createRawResponse.ExecuteCommand();


            } );

            //foreach (var file in files)
            //{
            //    var stream = new MemoryStream();
            //    await GetStreamFromFile(file.FullName, stream);

            //    using(stream)
            //    using (var sr = new StreamReader(stream))
            //    {
            //        var fileContent = await sr.ReadToEndAsync();
            //        var createRawResponse = new CreateRawResponse
            //        {

            //            Uri = "N\\A",
            //            Content = fileContent
            //        };

            //        await createRawResponse.ExecuteCommand();
            //    }

            //}



        }


        private async Task GetStreamFromFile(string filePath, Stream stream)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                await fileStream.CopyToAsync(stream);
            }
            stream.Seek(0, SeekOrigin.Begin);
        }

    }

    public class GetDatasetsByDatabase
    {
        public async Task Get(JsonResultDatabaseList databases)
        {
            foreach (var jsonDatabaseListDatabase in databases.Databases)
            {
                var requestDatasetList = new RequestDatabaseDatasetListBy(jsonDatabaseListDatabase.DatabaseCode);
                var datasetList = await requestDatasetList.ExecuteRequest();

                var datasets = GetCsvRows(datasetList.Datasets);
                foreach (var dataset in datasets)
                {
                    await new GetDatasetAndSaveToDb().ExecuteTask(dataset.DatabaseCode, dataset.DatasetCode);
                }


                // Parallel.ForEach(datasets, dataset =>
                //{
                //    new GetDatasetAndSaveToDb().ExecuteTask(dataset.DatabaseCode, dataset.DatasetCode);
                //});
                datasetList.Datasets.Dispose();
            }
        }

        private static IEnumerable<CsvDatabaseDataset> GetCsvRows(StreamReader stream)
        {

            string line;
            while ((line = stream.ReadLine()) != null)
            {
                var columns = line.Split(',');
                var splitQuandlCode = columns[0].Split('/');

                var dataset = new CsvDatabaseDataset
                {
                    DatabaseCode = splitQuandlCode[0],
                    DatasetCode = splitQuandlCode[1],
                    QuandlCode = columns[0],
                    DatasetDescription = columns[1]
                };
                yield return dataset;
            }



        }

    }


    public class GetDatasetAndSaveToDb
    {
        public Task ExecuteTask(string databaseCode, string datasetCode)
        {
            var task = Get(databaseCode, datasetCode);
            return Task.FromResult(task);
        }

        private async Task Get(string databaseCode, string datasetCode)
        {
            var requestDataset = new RequestDatasetDataAndMetadataBy(databaseCode, datasetCode);
            var datasetResponse = await requestDataset.GetString();
            if (!string.IsNullOrEmpty(datasetResponse.ContentString))
            {
                var createRawResponse = new CreateRawResponse
                {
                    Content = datasetResponse.ContentString,
                    Uri = requestDataset.ToUri()
                };
                await createRawResponse.ExecuteCommand();
            }
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