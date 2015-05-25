using NQuandl.Client.Requests;

namespace NQuandl.Client.Helpers
{
    public class QueryOptions
    {
        public SortOrder SortOrder { get; set; }
        public DateRange DateRange { get; set; }
        public int Column { get; set; }
        public Transformation Transformation { get; set; }
    }

    public static class QueryOptionsHelper
    {
        public static QueryParametersV1 GetOptionalRequestParameters(this QueryOptions options)
        {
            if (options == null) return null;
            var parameters = new QueryParametersV1
            {
                SortOrder = options.SortOrder,
                DateRange = options.DateRange,
                Column = options.Column,
                Transformation = options.Transformation
            };
            return parameters;
        }
    }
}