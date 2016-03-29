using System;
using System.Collections.Generic;
using System.Linq;
using Flurl;
using JetBrains.Annotations;
using NQuandl.Domain.Quandl.RequestParameters;
using NQuandl.Domain.Quandl.Requests;

namespace NQuandl.Api.Quandl.Helpers
{
    public static class UrlExtensions
    {
    
        public static Dictionary<string, string> ToRequestParameterDictionary(this RequestDatabaseMetadataBy query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var parameters = new List<RequestParameter>
            {
                new RequestParameter(RequestParameterConstants.DatabaseCode, query.DatabaseCode)
            };
            return parameters.ToDictionary(query.ApiKey);
        }


        public static Dictionary<string, string> ToRequestParameterDictionary(this RequestDatabaseListBy query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var parameters = new List<RequestParameter>();

            if (query.PerPage.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.PerPage, query.PerPage.Value.ToString());
                parameters.Add(parameter);
            }

            if (query.Page.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Page, query.Page.Value.ToString());
                parameters.Add(parameter);
            }

            return parameters.ToDictionary(query.ApiKey);

        }

        public static Dictionary<string, string> ToRequestParameterDictionary(this RequestDatabaseSearchBy query)
        {
            if (query == null) throw new NullReferenceException("options");

            var parameters = new List<RequestParameter>();

            if (!string.IsNullOrEmpty(query.Query))
            {
                var parameter = new RequestParameter(RequestParameterConstants.Query, query.Query);
                parameters.Add(parameter);
            }

            if (query.PerPage.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.PerPage, query.PerPage.Value.ToString());
                parameters.Add(parameter);
            }
            
            if (query.Page.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Page, query.Page.Value.ToString());
                parameters.Add(parameter);
            }

            return parameters.ToDictionary(query.ApiKey);
        }

        public static Dictionary<string, string> ToRequestParameterDictionary(
            [NotNull] this RequestDatasetMetadataBy query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var parameters = new List<RequestParameter>();

            return parameters.ToDictionary(query.ApiKey);
        }

        public static Dictionary<string, string> ToRequestParameterDictionary(this RequestDatasetDataBy query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var parameters = new List<RequestParameter>();

            if (query.Limit.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Limit, query.Limit.Value.ToString());
                parameters.Add(parameter);
            }

            if (query.Rows.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Rows, query.Rows.Value.ToString());
                parameters.Add(parameter);
            }

            if (query.ColumnIndex.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.ColumnIndex, query.ColumnIndex.Value.ToString());
                parameters.Add(parameter);
            }

            if (query.StartDate.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.StartDate, query.StartDate.Value.ToString("yyyy-mm-dd"));
                parameters.Add(parameter);
            }

            if (query.EndDate.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.EndDate, query.EndDate.Value.ToString("yyyy-mm-dd"));
                parameters.Add(parameter);
            }

            if (query.Order.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Order,
                    query.Order.Value.GetStringValue());
                parameters.Add(parameter);
            }

            if (query.Collapse.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Collapse, query.Collapse.Value.GetStringValue());
                parameters.Add(parameter);
            }

            if (query.Transform.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Transform, query.Transform.Value.GetStringValue());
                parameters.Add(parameter);
            }

            return parameters.ToDictionary(query.ApiKey);
        }

        public static Dictionary<string, string> ToDictionary(this List<RequestParameter> requestParameters, string apiKey)
        {
            if (!string.IsNullOrEmpty(apiKey))
            {
                requestParameters.Add(new RequestParameter(RequestParameterConstants.ApiKey, apiKey));
            }
            return requestParameters.ToDictionary(x => x.Name, x => x.Value);
        }
      
        public static string ToUrl(this QuandlClientRequestParameters parameters, string baseUrl)
        {
            if (string.IsNullOrEmpty(parameters.PathSegment)) throw new ArgumentException("Missing PathSegment");
            
            var url = baseUrl.AppendPathSegment(parameters.PathSegment);
            if (parameters.QueryParameters.Any())
            {
                url = url.SetQueryParams(parameters.QueryParameters);
            }

            return url;
        }

        public static string ToUri(this QuandlClientRequestParameters parameters, string apiKey = null)
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