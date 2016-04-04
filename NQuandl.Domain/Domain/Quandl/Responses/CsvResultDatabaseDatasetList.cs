using System.IO;

namespace NQuandl.Domain.Quandl.Responses
{
    public class CsvResultDatabaseDatasetList : ResultWithQuandlResponseInfo
    {
        public StreamReader Datasets { get; set; }
    }
}