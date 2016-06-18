using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json.Converters;
using NQuandl.Client.Api.Quandl.Helpers;
using NQuandl.Client.Domain.Requests;
using NQuandl.Client.Domain.Responses;
using NQuandl.Client.Services.Logger;
using NQuandl.Npgsql.Domain.Commands;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Domain.Queries;
using NQuandl.Npgsql.Services.Database;
using NQuandl.Npgsql.Services.Database.Configuration;
using NQuandl.Npgsql.Services.Extensions;
using NQuandl.Npgsql.Services.Mappers;
using NQuandl.Npgsql.Services.Metadata;
using NQuandl.SimpleClient;

namespace nquandl.console
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //var countries = new CountriesBy().ExecuteQuery();
            //countries.Subscribe((country => NonBlockingConsole.WriteLine(country.Name)));
            new DownloadAllGoldDatabases().Get().Wait();
           
            NonBlockingConsole.WriteLine("Done");
            Console.ReadLine();
        }
    }

    public class DownloadSampleGoldDatasetsFromDb
    {
        public async Task Get()
        {
            var dbContext = new DbContext(new DbConnectionProvider(new DebugConnectionConfiguration()), new SqlMapper());
            var databaseMetadataInitializer = new EntityMetadataCacheInitializer<Database>();
            var databaseMetadataCache = new EntityMetadataCache<Database>(databaseMetadataInitializer);

            var goldDatabasesQuery = new EntitiesObservableBy<Database>();
            var queryHandler = new HandleEntitiesObservableBy<Database>(databaseMetadataCache, dbContext);
            var result = await queryHandler.Handle(goldDatabasesQuery);
           


        }
    }


    public class DownloadAllGoldDatabases
    {
        public async Task Get()
        {
            for (var i = 1; i <= 2; i++)
            {
                var request = new RequestDatabaseSearchBy
                {
                    Query = "gold",
                    Page = i
                };

                var response = await request.ExecuteRequest();

                var dbContext = new DbContext(new DbConnectionProvider(new DebugConnectionConfiguration()),new SqlMapper());

                var databaseMetadataInitializer = new EntityMetadataCacheInitializer<Database>();
                var databaseMetadataCache = new EntityMetadataCache<Database>(databaseMetadataInitializer);
                var databaseDatasetMetadataCache = new EntityMetadataCache<DatabaseDataset>(new EntityMetadataCacheInitializer<DatabaseDataset>());
                var datasetMetadataCache = new EntityMetadataCache<Dataset>(new EntityMetadataCacheInitializer<Dataset>());

                var databases = new List<Database>();
                foreach (var jsonDatabaseSearchDatabase in response.JsonDatabaseSearchDatabases)
                {
                    var database = new Database
                    {
                        DatabaseCode = jsonDatabaseSearchDatabase.DatabaseCode,
                        DatasetsCount = jsonDatabaseSearchDatabase.DatasetsCount,
                        Description = jsonDatabaseSearchDatabase.Description,
                        Downloads = jsonDatabaseSearchDatabase.Downloads,
                        Id = jsonDatabaseSearchDatabase.Id,
                        Image = jsonDatabaseSearchDatabase.Image,
                        Premium = jsonDatabaseSearchDatabase.Premium,
                        Name = jsonDatabaseSearchDatabase.Name
                    };
                    databases.Add(database);
                    //var dbWriteDatabaseCommand = new WriteEntity<Database>(database);
                    //var dbWriteCommandHandler = new HandleWriteEntity<Database>(dbContext, databaseMetadataCache);
                    //await dbWriteCommandHandler.Handle(dbWriteDatabaseCommand);
                }

             
                var databaseDatasets = new List<DatabaseDataset>();
                foreach (var database in databases.OrderBy(x => x.DatasetsCount).Where(x => x.Premium == false))
                {
                    var datasetList = await new RequestDatabaseDatasetListBy(database.DatabaseCode).ExecuteRequest();
                    foreach (var dataset in datasetList.Datasets)
                    {
                        var databaseDataset = new DatabaseDataset
                        {
                            DatabaseCode = dataset.DatabaseCode,
                            DatasetCode = dataset.DatasetCode,
                            Description = dataset.DatasetDescription,
                            QuandlCode = dataset.QuandlCode,
              
                        };
                        var dbWriteDatabaseDataset = new WriteEntity<DatabaseDataset>(databaseDataset);
                        var handleDbWriteDatabaseDataset = new HandleWriteEntity<DatabaseDataset>(dbContext, databaseDatasetMetadataCache);
                        await handleDbWriteDatabaseDataset.Handle(dbWriteDatabaseDataset);
                        databaseDatasets.Add(databaseDataset);
                        NonBlockingConsole.WriteLine("DatabaseDataset: " + dataset.QuandlCode);
                        NonBlockingConsole.WriteLine("DatabaseDataset: " + dataset.DatasetDescription);
                       
                    }
                }

                foreach (var databaseDataset in databaseDatasets)
                {
                    var datasetResponse =
                        await
                            new RequestDatasetDataAndMetadataBy(databaseDataset.DatabaseCode,
                                databaseDataset.DatasetCode).ExecuteRequest();
                    var dataset = new Dataset
                    {
                        Code = datasetResponse.DataAndMetadata.DatasetCode,
                        ColumnNames = datasetResponse.DataAndMetadata.ColumnNames,
                        Data = datasetResponse.DataAndMetadata.Data,
                        DatabaseCode = datasetResponse.DataAndMetadata.DatabaseCode,
                        DatabaseId = datasetResponse.DataAndMetadata.DatabaseId,
                        Description = datasetResponse.DataAndMetadata.Description,
                        EndDate = datasetResponse.DataAndMetadata.EndDate,
                        Frequency = datasetResponse.DataAndMetadata.Frequency,
                        Id = datasetResponse.DataAndMetadata.Id,
                        Name = datasetResponse.DataAndMetadata.Name,
                        RefreshedAt = datasetResponse.DataAndMetadata.RefreshedAt,
                        StartDate = datasetResponse.DataAndMetadata.StartDate
                    };
                    var dbWriteDatasetCommand = new WriteEntity<Dataset>(dataset);
                    var handleWriteDatasetCommand = new HandleWriteEntity<Dataset>(dbContext,datasetMetadataCache);
                    await handleWriteDatasetCommand.Handle(dbWriteDatasetCommand);
                    NonBlockingConsole.WriteLine("Dataset: " + dataset.Name);
                    NonBlockingConsole.WriteLine("Dataset: " + dataset.Description);


                }
            }
          
        }
    }

    //public class DownloadAllDatasetsByDatabase
    //{
    //    public async Task Get(string databaseCode)
    //    {
    //        var request = new RequestDatabaseDatasetListBy(databaseCode);
    //        var datasetList = await request.ExecuteRequest();
    //        var command = new BulkCreateDatabaseDataset(datasetList.Datasets);
    //        await command.ExecuteCommand();

    //    }
    //}

    //public class GetAllDatasetCountries
    //{
    //    public async Task Get()
    //    {
    //        var countries = new CountriesBy().ExecuteQuery();

    //        var task = countries.Subscribe(
    //            async country => await new GetResultsFromDatasetByDescriptionContains().CreateDatasetCountries(country.Iso4217CountryName, country.Iso31661Alpha3), onError: exception => { throw new Exception(exception.Message); });

           
    //    }
    //}

    //public class GetResultsFromDatasetByDescriptionContains
    //{
    //    public async Task<IEnumerable<Dataset>> GetDatasets(string queryString)
    //    {
    //        var query = new DatasetsByDescriptionContains(queryString);
    //        var result = query.ExecuteQuery();
    //        var results = await result.ToList();

    //        return results;
    //    }

    //    public void PrintResults(IEnumerable<Dataset> results)
    //    {

    //        var count = 0;
    //        foreach (var dataset in results)
    //        {
    //            NonBlockingConsole.WriteLine(dataset.Description);
    //            count = count + 1;

    //        }
    //        NonBlockingConsole.WriteLine("Count: " + count);
    //    }

    //    public void CreateDatasetCountriesByAllCountries()
    //    {
    //        var countries = new CountriesBy().ExecuteQuery();

    //        countries.Subscribe(
    //            async country => await CreateDatasetCountries(country.Iso31661Alpha2, country.Iso31661Alpha3), onError: exception => {throw new Exception(exception.Message);});

    //    }

    //    public async Task CreateDatasetCountries(string queryString, string iso3166Alpha3)
    //    {
           
           
    //        var query = new DatasetsByDescriptionContains(queryString);
    //        var result = query.ExecuteQuery();

    //        var observable = Observable.Create<DatasetCountry>(
    //            obs => result.Subscribe(
    //                record => obs.OnNext(new DatasetCountry
    //                {
    //                    Association = queryString,
    //                    DatasetId = record.Id,
    //                    Iso31661Alpha3 = iso3166Alpha3
    //                }),
    //                onCompleted: obs.OnCompleted,
    //                onError: exception => { throw new Exception(exception.Message); }));

    //        var command = new BulkCreateDatasetCountries(observable);
    //        await command.ExecuteCommand();
    //    }

    //    public async Task GetAndPrint()
    //    {
    //        var results =  await GetDatasets("US");
    //        PrintResults(results);
    //    }
    //}


    //public class GetWikiCountriesFromJsonFile
    //{
    //    public IObservable<Country> GetCountryObservable(string filePath)
    //    {

    //        return Observable.Create<Country>(async obs =>
    //        {
    //            var wikiCountries = await new GetWikiCountriesFromJsonFile().GetWikiCountries(filePath);

    //            foreach (var binding in wikiCountries.results.bindings)
    //            {
    //                var country = new Country
    //                {
    //                    Name = binding.countryLabel.value,
    //                    Iso31661Alpha2 = binding.country_code_iso_3166_1_alpha_2.value,
    //                    Iso31661Alpha3 = binding.country_code_iso_3166_1_alpha_3.value,
    //                    Iso31661Numeric = int.Parse(binding.country_code_iso_3166_1_numeric.value),
    //                    CountryFlagUrl = binding.flag_imageImage.value
    //                };
    //                obs.OnNext(country);

    //            }
    //        });
    //    }

    //    public async Task<WikiCountries> GetWikiCountries(string filePath)
    //    {
    //        var memoryStream = new MemoryStream();
    //        await GetStreamFromFile(filePath, memoryStream);
    //        return memoryStream.DeserializeToEntity<WikiCountries>();
    //    }

    //    private async Task GetStreamFromFile(string filePath, Stream stream)
    //    {
    //        using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
    //        {
    //            await fileStream.CopyToAsync(stream);
    //        }
    //        stream.Seek(0, SeekOrigin.Begin);
    //    }
    //}



    //public class WikiCountries
    //{
    //    public Head head { get; set; }
    //    public Results results { get; set; }
    //}

    //public class Head
    //{
    //    public string[] vars { get; set; }
    //}

    //public class Results
    //{
    //    public Binding[] bindings { get; set; }
    //}

    //public class Binding
    //{
    //    public Country_Code_Iso_3166_1_Alpha_3 country_code_iso_3166_1_alpha_3 { get; set; }
    //    public Country_Code_Iso_3166_1_Numeric country_code_iso_3166_1_numeric { get; set; }
    //    public Country_Code_Iso_3166_1_Alpha_2 country_code_iso_3166_1_alpha_2 { get; set; }
    //    public Flag_Imageimage flag_imageImage { get; set; }
    //    public Countrylabel countryLabel { get; set; }
    //}

    //public class Country_Code_Iso_3166_1_Alpha_3
    //{
    //    public string type { get; set; }
    //    public string value { get; set; }
    //}

    //public class Country_Code_Iso_3166_1_Numeric
    //{
    //    public string type { get; set; }
    //    public string value { get; set; }
    //}

    //public class Country_Code_Iso_3166_1_Alpha_2
    //{
    //    public string type { get; set; }
    //    public string value { get; set; }
    //}

    //public class Flag_Imageimage
    //{
    //    public string type { get; set; }
    //    public string value { get; set; }
    //}

    //public class Countrylabel
    //{
    //    public string xmllang { get; set; }
    //    public string type { get; set; }
    //    public string value { get; set; }
    //}



    //public static class DeserializeAndConvertDataAndMetadataToDataset
    //{
    //    public static Dataset GetDataset(this RawResponse rawResponse)
    //    {
    //        try
    //        {
    //            NonBlockingConsole.WriteLine("deserializing: " + rawResponse.Id);
    //            var dataAndMetadata =
    //                rawResponse.ResponseContent.DeserializeToEntity<JsonResultDatasetDataAndMetadata>().DataAndMetadata;
    //            NonBlockingConsole.WriteLine("datasetcode: " + dataAndMetadata.DatasetCode);


    //            if (!dataAndMetadata.EndDate.HasValue || string.IsNullOrEmpty(dataAndMetadata.Frequency) ||
    //                !dataAndMetadata.StartDate.HasValue || !dataAndMetadata.RefreshedAt.HasValue)
    //            {
    //                return null;
    //            }

    //            return new Dataset
    //            {
    //                Id = dataAndMetadata.Id,
    //                Code = dataAndMetadata.DatasetCode,
    //                Data = dataAndMetadata.Data,
    //                DatabaseCode = dataAndMetadata.DatabaseCode,
    //                DatabaseId = dataAndMetadata.DatabaseId,
    //                Description = dataAndMetadata.Description,
    //                EndDate = dataAndMetadata.EndDate,
    //                Frequency = dataAndMetadata.Frequency,
    //                Name = dataAndMetadata.Name,
    //                RefreshedAt = dataAndMetadata.RefreshedAt,
    //                StartDate = dataAndMetadata.StartDate,
    //                ColumnNames = dataAndMetadata.ColumnNames
    //            };
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception(ex.Message);
    //        }
    //    }
    //}
}