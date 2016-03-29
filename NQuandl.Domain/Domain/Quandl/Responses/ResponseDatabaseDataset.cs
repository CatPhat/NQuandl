namespace NQuandl.Domain.Quandl.Responses
{
    public class ResponseDatabaseDataset : ResultWithQuandlResponseInfo
    {
        public JsonDatabaseDataset dataset { get; set; }
    }
}