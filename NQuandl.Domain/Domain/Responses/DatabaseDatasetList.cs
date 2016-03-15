using System;
using System.Collections.Generic;


namespace NQuandl.Domain.Responses
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

   


}