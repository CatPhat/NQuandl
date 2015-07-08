using System;
using System.Collections.Generic;
using System.Linq;
using NQuandl.Client.Domain.RequestParameters;

namespace NQuandl.Client.Api.Helpers
{
    public static class UrlExtensions
    {
        public static string ToPathSegment(this PathSegmentParametersV1 parameters)
        {
            var uri = parameters.ApiVersion +
                      "/" + parameters.QuandlCode +
                      "." + parameters.ResponseFormat;
            return uri;
        }

        public static string ToPathSegment(this PathSegmentParametersV2 parameters)
        {
            var uri = parameters.ApiVersion + "." + parameters.ResponseFormat;

            return uri;
        }

        public static Dictionary<string, string> ToQueryParameterDictionary(this RequestParametersV1 options)
        {
            if (options == null) throw new NullReferenceException("options");
            return options.ToQueryParameters().ToDictionary(x => x.Name, x => x.Value);
        }

        public static Dictionary<string, string> ToQueryParameterDictionary(this RequestParametersV2 options)
        {
            if (options == null) throw new NullReferenceException("options");
            return options.ToQueryParameters().ToDictionary(x => x.Name, x => x.Value);
        }

        public static IEnumerable<RequestParameter> ToQueryParameters(this RequestParametersV1 options)
        {
            var parameters = new List<RequestParameter>();

            if (!String.IsNullOrEmpty(options.ApiKey))
            {
                var parameter = new RequestParameter(RequestParameterConstants.AuthToken, options.ApiKey);
                parameters.Add(parameter);
            }
            if (options.SortOrder.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.SortOrder,
                    options.SortOrder.Value.GetStringValue());
                parameters.Add(parameter);
            }
            if (options.ExcludeHeaders.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.ExcludeHeaders,
                    options.ExcludeHeaders.Value.GetStringValue());
                parameters.Add(parameter);
            }
            if (options.ExcludeData.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.ExcludeData,
                    options.ExcludeData.Value.GetStringValue());
                parameters.Add(parameter);
            }
            if (options.Rows.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Rows, options.Rows.Value.ToString());
                parameters.Add(parameter);
            }
            if (options.Frequency.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Frequency,
                    options.Frequency.Value.ToString());
                parameters.Add(parameter);
            }
            if (options.DateRange != null)
            {
                const string dateFormat = "yyyy-MM-dd";

                var parameter1 = new RequestParameter(RequestParameterConstants.TrimStart,
                    options.DateRange.TrimStart.ToString(dateFormat));
                parameters.Add(parameter1);

                var parameter2 = new RequestParameter(RequestParameterConstants.TrimEnd,
                    options.DateRange.TrimEnd.ToString(dateFormat));
                parameters.Add(parameter2);
            }
            if (options.Column.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Column, options.Column.Value.ToString());
                parameters.Add(parameter);
            }
            if (options.Transformation.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Transformation,
                    options.Transformation.Value.GetStringValue());
                parameters.Add(parameter);
            }
            return parameters;
        }

        public static IEnumerable<RequestParameter> ToQueryParameters(this RequestParametersV2 options)
        {
            var parameters = new List<RequestParameter>
            {
                new RequestParameter(RequestParameterConstants.Query, options.Query),
                new RequestParameter(RequestParameterConstants.SourceCode, options.SourceCode),
                new RequestParameter(RequestParameterConstants.PerPage, options.PerPage.ToString()),
                new RequestParameter(RequestParameterConstants.Page, options.Page.ToString()),
                new RequestParameter(RequestParameterConstants.AuthToken, options.ApiKey)
            };

            return parameters;
        }
    }
}