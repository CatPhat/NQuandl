using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Flurl;
using NQuandl.Client.Requests;

namespace NQuandl.Client.Helpers
{
    public static class UrlExtensions
    {
        public static string ToUriV1(this RequiredRequestParameters parameters)
        {
            var uri = parameters.ApiVersion +
                      "/" + parameters.QuandlCode +
                      "." + parameters.ResponseFormat;
            return uri;
        }

        public static List<QueryParameter> ToQueryParameters(this OptionalRequestParameters optional)
        {
            var parameters = new List<QueryParameter>();

            if (!String.IsNullOrEmpty(optional.ApiKey))
            {
                var parameter = new QueryParameter(RequestParameterConstants.AuthToken, optional.ApiKey);
                parameters.Add(parameter);
            }
            if (optional.SortOrder.HasValue)
            {
                var parameter = new QueryParameter(RequestParameterConstants.SortOrder,
                    optional.SortOrder.Value.GetStringValue());
                parameters.Add(parameter);
            }
            if (optional.ExcludeHeaders.HasValue)
            {
                var parameter = new QueryParameter(RequestParameterConstants.ExcludeHeaders,
                    optional.ExcludeHeaders.Value.GetStringValue());
                parameters.Add(parameter);
            }
            if (optional.Rows.HasValue)
            {
                var parameter = new QueryParameter(RequestParameterConstants.Rows, optional.Rows.Value.ToString());
                parameters.Add(parameter);
            }
            if (optional.DateRange != null)
            {
                const string dateFormat = "yyyy-MM-dd";

                var parameter1 = new QueryParameter(RequestParameterConstants.TrimStart,
                    optional.DateRange.TrimStart.ToString(dateFormat));
                parameters.Add(parameter1);

                var parameter2 = new QueryParameter(RequestParameterConstants.TrimEnd,
                    optional.DateRange.TrimEnd.ToString(dateFormat));
                parameters.Add(parameter2);
            }
            if (optional.Column.HasValue)
            {
                var parameter = new QueryParameter(RequestParameterConstants.Column, optional.Column.Value.ToString());
                parameters.Add(parameter);
            }
            if (optional.Transformation.HasValue)
            {
                var parameter = new QueryParameter(RequestParameterConstants.Transformation,
                    optional.Transformation.Value.GetStringValue());
                parameters.Add(parameter);
            }
            return parameters;
        }

        public static string ToQueryUri(this OptionalRequestParameters optional)
        {
            var uri = string.Empty;
            if (optional == null) return uri;
            return uri.SetQueryParams(optional);
        }

        public static string ToQueryUri(this List<QueryParameter> parameters)
        {
            if (parameters.Count == 0)
            {
                return string.Empty;
            }

            var uri = new StringBuilder();

            var stringList =
                parameters.Select(serviceParameter => serviceParameter.Name + "=" + serviceParameter.Value).ToList();
            uri.Append("?");
            uri.Append(stringList.First());
            foreach (var parameter in parameters.Skip(1))
            {
                uri.Append("&" + parameter.Name + "=" + parameter.Value);
            }

            return uri.ToString();
        }
    }
}