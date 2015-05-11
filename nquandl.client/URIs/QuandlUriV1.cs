using System.Collections.Generic;
using NQuandl.Client.Helpers;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;

namespace NQuandl.Client.URIs
{
    public class QuandlUriV1 : IContainUri
    {
        private readonly RequestOptionsV1 _options;
        private readonly PathSegmentParametersV1 _required;

        public QuandlUriV1(string quandlCode, ResponseFormat format, RequestOptionsV1 options = null)
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
            get { return _required.ToUri(); }
        }

        public IEnumerable<QueryParameter> QueryParmeters
        {
            get { return _options.ToQueryParameters(); }
        }
    }
}