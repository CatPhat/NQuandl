using System.Collections.Generic;

namespace NQuandl.Domain.Quandl.Responses
{
    public class CsvResultDatabaseDatasetList : ResultWithQuandlResponseInfo
    {
        public IEnumerable<CsvDatabaseDataset> Datasets { get; set; }
    }
}