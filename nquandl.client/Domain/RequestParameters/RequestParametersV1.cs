using NQuandl.Client.Api.Helpers;

namespace NQuandl.Client.Domain.RequestParameters
{
    public class RequestParametersV1
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
}