using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client.Helpers
{
    public static class OptionalRequestBuilder
    {
        public static string ToRequestParameter(this OptionalRequestParameters optionalRequestParameters)
        {
            if (optionalRequestParameters == null)
            {
                return null;
            }

            var parameters = new List<string>();
            var parameter = new StringBuilder();

            if (optionalRequestParameters.SortOrder.HasValue)
            {
                parameters.Add(RequestParameter.SortOrder(optionalRequestParameters.SortOrder.Value));
            }
            if (optionalRequestParameters.ExcludeHeaders.HasValue)
            {
                parameters.Add(RequestParameter.ExcludeHeaders(optionalRequestParameters.ExcludeHeaders.Value));
            }
            if (optionalRequestParameters.Rows.HasValue)
            {
                parameters.Add(RequestParameter.Rows(optionalRequestParameters.Rows.Value));
            }
            if (optionalRequestParameters.DateRange != null)
            {
                parameters.Add(RequestParameter.DateRange(optionalRequestParameters.DateRange));
            }
            if (optionalRequestParameters.Column.HasValue)
            {
                parameters.Add(RequestParameter.Column(optionalRequestParameters.Column.Value));
            }
            if (optionalRequestParameters.Transformation.HasValue)
            {
                parameters.Add(RequestParameter.Transformation(optionalRequestParameters.Transformation.Value));
            }

            if (parameters.Count <= 1) return parameters.FirstOrDefault();

            foreach (var requestParameter in parameters)
            {
                parameter.Append("&" + requestParameter);
            }

            return parameter.ToString();
        }
    }
}
