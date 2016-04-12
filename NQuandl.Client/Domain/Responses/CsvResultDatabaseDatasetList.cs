using System.IO;

namespace NQuandl.Client.Domain.Responses
{
    public class CsvResultDatabaseDatasetList : ResultWithQuandlResponseInfo
    {
        public StreamReader Datasets { get; set; }
    }
}