namespace NQuandl.Domain.Quandl.Responses
{
    public class JsonResultDatabaseMetadata : ResultWithQuandlResponseInfo
    {
        public JsonDatabaseMetadata Metadata { get; set; }
    }
}