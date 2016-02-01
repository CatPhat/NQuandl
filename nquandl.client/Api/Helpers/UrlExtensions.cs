using System;
using System.Collections.Generic;
using System.Linq;
using Flurl;
using NQuandl.Client.Domain.RequestParameters;

namespace NQuandl.Client.Api.Helpers
{
    public static class UrlExtensions
    {
        public static string ToPathSegment(this PathSegmentParameters parameters)
        {
            var uri = parameters.ApiVersion +
                      "/" + parameters.QuandlCode +
                      "." + parameters.ResponseFormat.ToLower();
            return uri;
        }

        public static Dictionary<string, string> ToQueryParameterDictionary(this RequestParameters options)
        {
            if (options == null) throw new NullReferenceException("options");
            return options.ToQueryParameters().ToDictionary(x => x.Name, x => x.Value);
        }

        public static IEnumerable<RequestParameter> ToQueryParameters(this RequestParameters options)
        {
            var parameters = new List<RequestParameter>();

            if (options.Limit.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Limit, options.Limit.Value.ToString());
                parameters.Add(parameter);
            }

            if (options.Rows.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Rows, options.Rows.Value.ToString());
                parameters.Add(parameter);
            }

            if (options.ColumnIndex.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.ColumnIndex, options.ColumnIndex.Value.ToString());
                parameters.Add(parameter);
            }

            if (options.StartDate.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.StartDate, options.StartDate.Value.ToString("yyyy-mm-dd"));
                parameters.Add(parameter);
            }

            if (options.EndDate.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.EndDate, options.EndDate.Value.ToString("yyyy-mm-dd"));
                parameters.Add(parameter);
            }

            if (options.Order.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Order,
                    options.Order.Value.GetStringValue());
                parameters.Add(parameter);
            }

            if (options.Collapse.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Collapse, options.Collapse.Value.GetStringValue());
                parameters.Add(parameter);
            }

            if (options.Transform.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Transform, options.Transform.Value.GetStringValue());
                parameters.Add(parameter);
            }

            if (!string.IsNullOrEmpty(options.ApiKey))
            {
                var parameter = new RequestParameter(RequestParameterConstants.ApiKey, options.ApiKey);
                parameters.Add(parameter);
            }
         
            return parameters;
        }

      

        public static string ToUrl(this QuandlRestClientRequestParameters parameters, string baseUrl)
        {
            if (string.IsNullOrEmpty(parameters.PathSegment)) throw new ArgumentException("Missing PathSegment");
            
            var url = baseUrl.AppendPathSegment(parameters.PathSegment);
            if (parameters.QueryParameters.Any())
            {
                url = url.SetQueryParams(parameters.QueryParameters);
            }

            return url;
        }

        public static string ToUri(this QuandlRestClientRequestParameters parameters)
        {
            if (string.IsNullOrEmpty(parameters.PathSegment)) throw new ArgumentException("Missing PathSegment");

            var url = "api".AppendPathSegment(parameters.PathSegment);
            if (parameters.QueryParameters.Any())
            {
                url = url.SetQueryParams(parameters.QueryParameters);
            }

            return url;
        }
    }
}