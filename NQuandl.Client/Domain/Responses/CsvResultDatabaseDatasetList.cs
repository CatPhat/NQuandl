using System.Collections.Generic;
using System.IO;

namespace NQuandl.Client.Domain.Responses
{
    public class CsvResultDatabaseDatasetList : ResultWithQuandlResponseInfo
    {
        public IEnumerable<CsvDatabaseDataset> Datasets { get; set; }
    }
}