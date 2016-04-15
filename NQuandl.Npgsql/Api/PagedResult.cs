namespace NQuandl.Npgsql.Api
{
    public abstract class PagedResult
    {
        public int? Offset { get; set; }
        public int? Limit { get; set; }
        public string OrderBy { get; set; }
    }
}