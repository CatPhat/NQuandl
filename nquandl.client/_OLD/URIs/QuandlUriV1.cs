using System.Collections.Generic;
using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;

namespace NQuandl.Client.URIs
{
    public class QuandlUriV1 : IQuandlUri
    {
        private readonly QueryParametersV1 _options;
        private readonly PathSegmentParametersV1 _required;

        public QuandlUriV1(string quandlCode, ResponseFormat format, QueryParametersV1 options = null)
        {
            _required = new PathSegmentParametersV1
            {
                ApiVersion = RequestParameterConstants.ApiVersion1,
                ResponseFormat = format.GetStringValue(),
                QuandlCode = quandlCode
            };
            _options = options;
        }

        public string PathSegment
        {
            get { return _required.ToPathSegment(); }
        }

        public Dictionary<string, string> QueryParmeters
        {
            get { return _options.ToQueryParameterDictionary(); }
        }
    }
}