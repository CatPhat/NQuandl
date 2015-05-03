using System;
using NQuandl.Client.Helpers;

namespace NQuandl.Client.Requests
{
    public class RequestParameters
    {
        
        public string ApiKey { get; set; }
        public string BaseUrl { get; set; }
        public string QuandlCode { get; set; }
        public string RequestFormat { get; set; }
        public string ApiVersion { get; set; }
        public string ResponseFormat { get; set; }
        public OptionalRequestParameters Options { get; set; }
    }

    public class OptionalRequestParameters
    {
        public SortOrder? SortOrder { get; set; }
        public Exclude? ExcludeHeaders { get; set; }
        public int? Rows { get; set; }
        public DateRange DateRange { get; set; }
        public int? Column { get; set; }
        public Transformation? Transformation { get; set; }
        public Exclude? ExcludeData { get; set; }
    }

    public class DateRange
    {
        public DateTime TrimStart { get; set; }
        public DateTime TrimEnd { get; set; }
    }
}