using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client._OLD.Requests;

namespace NQuandl.Client.Api.Helpers
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