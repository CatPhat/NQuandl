using System.Collections.Generic;
using NQuandl.Client.Helpers;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;

namespace NQuandl.Client.URIs
{
    public class QuandlUriV2 : IContainUri
    {
        private readonly RequestOptionsV2 _options;
        private readonly PathSegmentParametersV2 _required;

        public QuandlUriV2(RequestOptionsV2 options, ResponseFormat format)
        {
            _required = new PathSegmentParametersV2
            {
                ApiVersion = RequestParameterConstants.ApiVersion2,
                ResponseFormat = format.GetStringValue(),
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