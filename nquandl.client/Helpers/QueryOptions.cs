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
        public static RequestParameterOptions GetOptionalRequestParameters(this QueryOptions options)
        {
            if (options == null) return null;
            var parameters = new RequestParameterOptions
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