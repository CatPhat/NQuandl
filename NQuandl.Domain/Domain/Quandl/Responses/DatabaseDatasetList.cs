using System.Collections.Generic;

namespace NQuandl.Domain.Quandl.Responses
{
    public class DatabaseDatasetList : ResultWithQuandlResponseInfo
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