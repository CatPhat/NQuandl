namespace NQuandl.Domain.Quandl.Responses
{
    public class JsonResultDatabaseList : ResultWithQuandlResponseInfo
    {
        public JsonDatabaseListDatabase[] Databases { get; set; }
        public JsonDatabaseListMetadata Metadata { get; set; }
    }
}