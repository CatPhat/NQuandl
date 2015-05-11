using System;
using NQuandl.Client.Helpers;

namespace NQuandl.Client.Requests
{
    public class RequestOptionsV1
    {
        public string ApiKey { get; set; }
        public SortOrder? SortOrder { get; set; }
        public Exclude? ExcludeHeaders { get; set; }
        public int? Rows { get; set; }
        public DateRange DateRange { get; set; }
        public int? Column { get; set; }
        public Transformation? Transformation { get; set; }
        public Exclude? ExcludeData { get; set; }
        public Frequency? Frequency { get; set; }
    }

    public class RequestOptionsV2
    {
        public string ApiKey { get; set; }
        public string Query { get; set; }
        public string SourceCode { get; set; }
        public int? PerPage { get; set; }
        public int? Page { get; set; }
    }

    public class ForcedRequestOptionsV2 : RequestOptionsV2
    {
        public ForcedRequestOptionsV2(string apiKey, string query, string sourceCode, int perPage, int page)
        {
            ApiKey = apiKey;
            Query = query;
            SourceCode = sourceCode;
            PerPage = perPage;
            Page = page;
        }
    }

    public class DateRange
    {
        public DateTime TrimStart { get; set; }
        public DateTime TrimEnd { get; set; }
    }
}