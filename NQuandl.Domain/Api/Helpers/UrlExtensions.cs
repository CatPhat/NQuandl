using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NQuandl.Domain.Queries;
using NQuandl.Domain.RequestParameters;
using NQuandl.Domain.Responses;
using Flurl;
using System.Linq;

namespace NQuandl.Api.Helpers
{
    public static class UrlExtensions
    {
        public static QuandlClientRequestParameters ToQuandlClientRequestParameters(this DatabaseDatasetListBy query)
        {
            return new QuandlClientRequestParameters
            {
                PathSegment = query.ToPathSegment(),
                QueryParameters = new Dictionary<string, string>()
            };
        }

        public static QuandlClientRequestParameters ToQuandlClientRequestParameters(this DatabaseListBy query)
        {
            return new QuandlClientRequestParameters
            {
                PathSegment = query.ToPathSegment(),
                QueryParameters = query.ToRequestParameterDictionary()
            };
        }

        public static QuandlClientRequestParameters ToQuandlClientRequestParameters(this DatabaseMetadataBy query)
        {
            return new QuandlClientRequestParameters
            {
                PathSegment = query.ToPathSegment(),
                QueryParameters = query.ToRequestParameterDictionary()
            };
        }


        public static QuandlClientRequestParameters ToQuandlClientRequestParameters(this DatabaseSearchBy query)
        {
            return new QuandlClientRequestParameters
            {
                PathSegment = query.ToPathSegment(),
                QueryParameters = query.ToRequestParameterDictionary()
            };
        }

        public static QuandlClientRequestParameters ToQuandlClientRequestParameters<TEntity>(this DatasetBy<TEntity> query) where TEntity : QuandlEntity
        {
            return new QuandlClientRequestParameters
            {
                PathSegment = query.ToPathSegment(),
                QueryParameters = query.ToRequestParameterDictionary()
            };
        }


        // https://www.quandl.com/api/v3/databases/:database_code/codes
        public static string ToPathSegment(this DatabaseDatasetListBy query)
        {
            return $"{query.ApiVersion}/databases/{query.DatabaseCode}/codes";
        }

        // https://www.quandl.com/api/v3/databases.json
        public static string ToPathSegment(this DatabaseListBy query)
        {
            return $"{query.ApiVersion}/databases.{query.ResponseFormat.GetStringValue()}";
        }

        // https://www.quandl.com/api/v3/databases/WIKI.json
        public static string ToPathSegment(this DatabaseMetadataBy query)
        {
            return $"{query.ApiVersion}/databases/{query.DatabaseCode}.{query.ResponseFormat.GetStringValue()}";
        }
        
        // https://www.quandl.com/api/v3/databases.json
        public static string ToPathSegment(this DatabaseSearchBy query)
        {
            return $"{query.ApiVersion}/databases.{query.ResponseFormat.GetStringValue()}";
        }

        // https://www.quandl.com/api/v3/datasets/WIKI/FB.json
        public static string ToPathSegment<TEntity>(this DatasetBy<TEntity> query) where TEntity : QuandlEntity
        {
            var entity = (TEntity)Activator.CreateInstance(typeof(TEntity));
            return $"{query.ApiVersion}/datasets/{entity.DatabaseCode}/{entity.DatasetCode}.{query.ResponseFormat.GetStringValue()}";
        }

      
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

            if (!String.IsNullOrEmpty(query.Query))
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

      

        public static string ToUrl(this QuandlClientRequestParameters parameters, string baseUrl)
        {
            if (String.IsNullOrEmpty(parameters.PathSegment)) throw new ArgumentException("Missing PathSegment");
            
            var url = baseUrl.AppendPathSegment(parameters.PathSegment);
            if (parameters.QueryParameters.Any())
            {
                url = url.SetQueryParams(parameters.QueryParameters);
            }

            return url;
        }

        public static string ToUri(this QuandlClientRequestParameters parameters, string apiKey = null)
        {
            if (String.IsNullOrEmpty(parameters.PathSegment)) throw new ArgumentException("Missing PathSegment");

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



        //public static async Task<TResult> DeserializeToJsonResultAsync<TResult>(this HttpResponseMessage response) where TResult : ResponseWithRawHttpContent
        //{
        //    var result = JsonConvert.DeserializeObject<TResult>(await response.Content.ReadAsStringAsync());
        //    result.HttpResponseMessage = response;
        //    return result;
        //}
    }
}