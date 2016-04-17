using System;
using System.Reactive.Linq;
using NQuandl.Client.Api.Quandl.Helpers;
using NQuandl.Client.Domain.Responses;
using NQuandl.Client.Services.Logger;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Domain.Queries;
using NQuandl.SimpleClient;

namespace nquandl.console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var query = new DatabasesBy {Limit = 10};

            var result = query.ExecuteQuery().ToEnumerable();
            foreach (var database in result)
            {
                NonBlockingConsole.WriteLine(database.Name);
            }


            NonBlockingConsole.WriteLine("Done");
            Console.ReadLine();
        }
    }

    public static class DeserializeAndConvertDataAndMetadataToDataset
    {
        public static Dataset GetDataset(this RawResponse rawResponse)
        {
            try
            {
                NonBlockingConsole.WriteLine("deserializing: " + rawResponse.Id);
                var dataAndMetadata =
                    rawResponse.ResponseContent.DeserializeToEntity<JsonResultDatasetDataAndMetadata>().DataAndMetadata;
                NonBlockingConsole.WriteLine("datasetcode: " + dataAndMetadata.DatasetCode);


                if (!dataAndMetadata.EndDate.HasValue || string.IsNullOrEmpty(dataAndMetadata.Frequency) ||
                    !dataAndMetadata.StartDate.HasValue || !dataAndMetadata.RefreshedAt.HasValue)
                {
                    return null;
                }

                return new Dataset
                {
                    Id = dataAndMetadata.Id,
                    Code = dataAndMetadata.DatasetCode,
                    Data = dataAndMetadata.Data,
                    DatabaseCode = dataAndMetadata.DatabaseCode,
                    DatabaseId = dataAndMetadata.DatabaseId,
                    Description = dataAndMetadata.Description,
                    EndDate = dataAndMetadata.EndDate,
                    Frequency = dataAndMetadata.Frequency,
                    Name = dataAndMetadata.Name,
                    RefreshedAt = dataAndMetadata.RefreshedAt,
                    StartDate = dataAndMetadata.StartDate,
                    ColumnNames = dataAndMetadata.ColumnNames
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}