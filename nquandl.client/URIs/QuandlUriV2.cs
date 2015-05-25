using System.Collections.Generic;
using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;

namespace NQuandl.Client.URIs
{
    public class QuandlUriV2 : IQuandlUri
    {
        private readonly PathSegmentParametersV2 _pathSegmentParameters;
        private readonly QueryParametersV2 _queryParameters;

        public QuandlUriV2(ResponseFormat format, QueryParametersV2 queryParameters)
        {
            _pathSegmentParameters = new PathSegmentParametersV2
            {
                ApiVersion = RequestParameterConstants.ApiVersion2,
                ResponseFormat = format.GetStringValue(),
            };
            _queryParameters = queryParameters;
        }

        public string PathSegment
        {
            get { return _pathSegmentParameters.ToUri(); }
        }

        public Dictionary<string, string> QueryParmeters
        {
            get { return _queryParameters.ToQueryParameterDictionary(); }
        }
    }
}