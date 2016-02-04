using System;
using System.Collections.Generic;
using System.Linq;
using Flurl;
using NQuandl.Client.Domain.RequestParameters;
using static System.String;


namespace NQuandl.Client.Api.Helpers
{
    public static class UrlExtensions
    {
        // https://www.quandl.com/api/v3/datasets/WIKI/FB.json
        public static string ToPathSegment(this RequiredDataRequestParameters parameters)
        {
            return
                $"{parameters.ApiVersion}/datasets/{parameters.DatabaseCode}/{parameters.DatasetCode}.{parameters.ResponseFormat.GetStringValue()}";
        }

        // https://www.quandl.com/api/v3/databases.json
        public static string ToPathSegment(this DatabaseListRequestParameters parameters)
        {
            return $"{parameters.ApiVersion}/databases.{parameters.ResponseFormat.GetStringValue()}";
        }

        // https://www.quandl.com/api/v3/databases/WIKI.json
        public static string ToPathSegment(this DatabaseMetadataRequestParameters parameters)
        {
            return $"{parameters.ApiVersion}/databases/{parameters.DatabaseCode}.{parameters.ResponseFormat.GetStringValue()}";
        }

        // https://www.quandl.com/api/v3/databases.json
        public static string ToPathSegment(this DatabaseSearchRequestParameters parameters)
        {
            return $"{parameters.ApiVersion}/databases.{parameters.ResponseFormat.GetStringValue()}";
        }

        // todo: consolidate
        public static Dictionary<string, string> ToRequestParameterDictionary(this OptionalDataRequestParameters options)
        {
            return options?.ToRequestParameters().ToDictionary(x => x.Name, x => x.Value) ?? new Dictionary<string, string>();
        }

        public static Dictionary<string, string> ToRequestParameterDictionary(this DatabaseListRequestParameters options)
        {
            return options?.ToRequestParameters().ToDictionary(x => x.Name, x => x.Value) ??
                   new Dictionary<string, string>();
        }

        public static Dictionary<string, string> ToRequestParameterDictionary(this DatabaseMetadataRequestParameters options)
        {
            return options?.ToRequestParameters().ToDictionary(x => x.Name, x => x.Value) ??
                   new Dictionary<string, string>();
        }

        public static Dictionary<string, string> ToRequestParameterDictionary(this DatabaseSearchRequestParameters options)
        {
            return options?.ToRequestParameters().ToDictionary(x => x.Name, x => x.Value) ??
                   new Dictionary<string, string>();
        }
        

        //end consolidate

        public static IEnumerable<RequestParameter> ToRequestParameters(this DatabaseMetadataRequestParameters options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            var parameters = new List<RequestParameter>
            {
                new RequestParameter(RequestParameterConstants.DatabaseCode, options.DatabaseCode)
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

        public static IEnumerable<RequestParameter> ToRequestParameters(this DatabaseListRequestParameters options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            var parameters = new List<RequestParameter>();

            if (options.PerPage.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.PerPage, options.PerPage.Value.ToString());
                parameters.Add(parameter);
            }

            if (options.Page.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Page, options.Page.Value.ToString());
                parameters.Add(parameter);
            }

            return parameters;
        
        }

        public static IEnumerable<RequestParameter> ToRequestParameters(this DatabaseSearchRequestParameters options)
        {
            if (options == null) throw new NullReferenceException("options");

            var parameters = new List<RequestParameter>();

            if (!IsNullOrEmpty(options.Query))
            {
                var parameter = new RequestParameter(RequestParameterConstants.Query, options.Query);
                parameters.Add(parameter);
            }

            if (options.PerPage.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.PerPage, options.PerPage.Value.ToString());
                parameters.Add(parameter);
            }
            
            if (options.Page.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Page, options.Page.Value.ToString());
                parameters.Add(parameter);
            }

           return parameters;
        }

        public static IEnumerable<RequestParameter> ToRequestParameters(this OptionalDataRequestParameters options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

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

        public static string ToUri(this QuandlRestClientRequestParameters parameters)
        {
            if (IsNullOrEmpty(parameters.PathSegment)) throw new ArgumentException("Missing PathSegment");

            var url = "api".AppendPathSegment(parameters.PathSegment);
            if (parameters.QueryParameters.Any())
            {
                url = url.SetQueryParams(parameters.QueryParameters);
            }

            return url;
        }
    }
}