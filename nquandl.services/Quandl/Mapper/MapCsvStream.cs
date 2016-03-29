using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Api;
using NQuandl.Api.Quandl;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Services.Quandl.Mapper
{
    public class MapCsvStream : IMapCsvStream
    {
        public async Task<IEnumerable<CsvDatabaseDataset>> MapToDataset(StreamReader stream)
        {
            var result = await stream.ReadToEndAsync();

            var rows = result.Split('\n');
            var datasets = (from row in rows
                select row.Split(',')
                into columns
                where columns.Length == 2
                let splitCode = columns[0].Split('/')
                select new CsvDatabaseDataset
                {
                    QuandlCode = columns[0],
                    DatabaseCode = splitCode[0],
                    DatasetCode = splitCode[1],
                    DatasetDescription = columns[1]
                });
            stream.Close();
            return datasets;
        }
    }
}