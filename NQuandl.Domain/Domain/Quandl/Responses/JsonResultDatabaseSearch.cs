namespace NQuandl.Domain.Quandl.Responses
{
    public class JsonResultDatabaseSearch : ResultWithQuandlResponseInfo
    {
        public JsonSearchDatabase[] Databases { get; set; }
        public JsonSearchMetadata Metadata { get; set; }
    }
}