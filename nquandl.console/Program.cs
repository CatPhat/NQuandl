using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NQuandl.Client.Api.Quandl.Helpers;
using NQuandl.Client.Domain.Requests;
using NQuandl.Client.Domain.Responses;
using NQuandl.Client.Services.Logger;

using NQuandl.Npgsql.Domain.Entities;

using NQuandl.Npgsql.Services.Extensions;
using NQuandl.Npgsql.Services.Mappers;
using NQuandl.SimpleClient;

namespace nquandl.console
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //var countries = new CountriesBy().ExecuteQuery();
            //countries.Subscribe((country => NonBlockingConsole.WriteLine(country.Name)));

          
            NonBlockingConsole.WriteLine("Done");
            Console.ReadLine();
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