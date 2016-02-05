using System;
using System.Collections.Generic;
using System.Linq;
using Flurl;
using NQuandl.Client.Domain.QuandlQueries;
using NQuandl.Client.Domain.RequestParameters;
using static System.String;


namespace NQuandl.Client.Api.Helpers
{
    public static class UrlExtensions
    { 
      
     
        // todo: consolidate
        public static Dictionary<string, string> ToRequestParameterDictionary<TEntity>(this DatasetBy<TEntity> options) where TEntity : QuandlEntity
        {
            return options?.ToRequestParameters().ToDictionary(x => x.Name, x => x.Value) ?? new Dictionary<string, string>();
        }

        public static Dictionary<string, string> ToRequestParameterDictionary(this DatabaseListBy query)
        {
            return query?.ToRequestParameters().ToDictionary(x => x.Name, x => x.Value) ??
                   new Dictionary<string, string>();
        }

        public static Dictionary<string, string> ToRequestParameterDictionary(this DatabaseMetadataBy query)
        {
            return query?.ToRequestParameters().ToDictionary(x => x.Name, x => x.Value) ??
                   new Dictionary<string, string>();
        }

        public static Dictionary<string, string> ToRequestParameterDictionary(this DatabaseSearchBy query)
        {
            return query?.ToRequestParameters().ToDictionary(x => x.Name, x => x.Value) ??
                   new Dictionary<string, string>();
        }
        

        //end consolidate

        public static IEnumerable<RequestParameter> ToRequestParameters(this DatabaseMetadataBy query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var parameters = new List<RequestParameter>
            {
                new RequestParameter(RequestParameterConstants.DatabaseCode, query.DatabaseCode)
            };
            return parameters;
        }

        public static IEnumerable<RequestParameter> ToRequestParameters(this DatasetMetadataRequestParameters options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            var parameters = new List<RequestParameter>
            {
                new RequestParameter(RequestParameterConstants.DatabaseCode, options.DatabaseCode),
                new RequestParameter(RequestParameterConstants.DatasetCode, options.DatasetCode)
            };

            return parameters;
        }

        public static IEnumerable<RequestParameter> ToRequestParameters(this DatabaseListBy query)
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

            return parameters;
        
        }

        public static IEnumerable<RequestParameter> ToRequestParameters(this DatabaseSearchBy query)
        {
            if (query == null) throw new NullReferenceException("options");

            var parameters = new List<RequestParameter>();

            if (!IsNullOrEmpty(query.Query))
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

           return parameters;
        }

        public static IEnumerable<RequestParameter> ToRequestParameters<TEntity>(this DatasetBy<TEntity> query) where TEntity : QuandlEntity
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
         
            return parameters;
        }

      

        public static string ToUrl(this QuandlRestClientRequestParameters parameters, string baseUrl)
        {
            if (IsNullOrEmpty(parameters.PathSegment)) throw new ArgumentException("Missing PathSegment");
            
            var url = baseUrl.AppendPathSegment(parameters.PathSegment);
            if (parameters.QueryParameters.Any())
            {
                url = url.SetQueryParams(parameters.QueryParameters);
            }

            return url;
        }

        public static string ToUri(this QuandlRestClientRequestParameters parameters, string apiKey = null)
        {
            if (IsNullOrEmpty(parameters.PathSegment)) throw new ArgumentException("Missing PathSegment");

            var url = "api".AppendPathSegment(parameters.PathSegment);

            if (!string.IsNullOrEmpty(apiKey))
            {
                parameters.QueryParameters.Add(RequestParameterConstants.ApiKey, apiKey);
            }

            if (parameters.QueryParameters.Any())
            {
                url = url.SetQueryParams(parameters.QueryParameters);
            }

            return url;
        }
    }
}