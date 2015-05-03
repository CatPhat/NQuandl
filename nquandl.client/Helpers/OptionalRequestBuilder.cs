using System.Collections.Generic;
using System.Linq;
using System.Text;
using NQuandl.Client.Requests;

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
                parameters.Add(RequestParameterHelper.SortOrder(optionalRequestParameters.SortOrder.Value));
            }
            if (optionalRequestParameters.ExcludeHeaders.HasValue)
            {
                parameters.Add(RequestParameterHelper.ExcludeHeaders(optionalRequestParameters.ExcludeHeaders.Value));
            }
            if (optionalRequestParameters.Rows.HasValue)
            {
                parameters.Add(RequestParameterHelper.Rows(optionalRequestParameters.Rows.Value));
            }
            if (optionalRequestParameters.DateRange != null)
            {
                parameters.Add(RequestParameterHelper.DateRange(optionalRequestParameters.DateRange));
            }
            if (optionalRequestParameters.Column.HasValue)
            {
                parameters.Add(RequestParameterHelper.Column(optionalRequestParameters.Column.Value));
            }
            if (optionalRequestParameters.Transformation.HasValue)
            {
                parameters.Add(RequestParameterHelper.Transformation(optionalRequestParameters.Transformation.Value));
            }

            if (parameters.Count <= 1) return parameters.FirstOrDefault();

            parameter.Append(parameters.First());
            foreach (var requestParameter in parameters.Skip(1))
            {
                parameter.Append("&" + requestParameter);
            }

            return parameter.ToString();
        }
    }
}