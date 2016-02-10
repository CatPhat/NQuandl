using System;
using System.Collections.Generic;
using CsvHelper.Configuration;

namespace NQuandl.Client.Domain.Responses
{
    public class DatabaseDatasetList : ResponseWithHttpMessage
    {
        public IEnumerable<DatabaseDatasetCsvRow> Datasets { get; set; }
    }

    public class DatabaseDatasetCsvRow
    {
        public string DatabaseCode { get; set; }
        public string QuandlCode { get; set; }
        public string DatasetCode { get; set; }
        public string DatasetDescription { get; set; }
    }

    //http://joshclose.github.io/CsvHelper/
    public sealed class DatabaseDatasetCsvRowMapper: CsvClassMap<DatabaseDatasetCsvRow>
    {
        public DatabaseDatasetCsvRowMapper()
        {
            Map(m => m.QuandlCode).Index(0);
            Map(m => m.DatasetDescription).Index(1);

            Map(m => m.DatabaseCode).ConvertUsing(r =>
            {
                var rowString = r.GetField(0);
                var index = rowString.IndexOf("/", StringComparison.Ordinal);
                return index < 0 ? "" : rowString.Substring(0, index);
            });

            Map(m => m.DatasetCode).Index(0).ConvertUsing(r =>
            {
                var rowString = r.GetField(0);
                var index = rowString.IndexOf("/", StringComparison.Ordinal) + 1;
                return index < 0 ? "" : rowString.Substring(index, rowString.Length - index);
            });

        }
    }


}