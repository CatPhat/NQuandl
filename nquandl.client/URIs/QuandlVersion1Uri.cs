using System;
using System.Collections.Generic;
using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;

namespace NQuandl.Client.URIs
{
    public interface IContainUri
    {
        string PathSegment { get; }
        IEnumerable<QueryParameter> QueryParmeters { get; }
    }

    public class QuandlVersion1Uri : IContainUri
    {
        private readonly RequestParameterOptions _options;
        private readonly RequiredRequestParameters _required;

        public QuandlVersion1Uri(string quandlCode, ResponseFormat format, RequestParameterOptions options = null)
        {
            _required = new RequiredRequestParameters
            {
                ApiVersion = RequestParameterConstants.ApiVersion1,
                ResponseFormat = format.GetStringValue(),
                QuandlCode = quandlCode
            };
            _options = options;
        }

        public string PathSegment
        {
            get { return _required.ToUriV1(); }
        }

        public IEnumerable<QueryParameter> QueryParmeters
        {
            get { return _options.ToQueryParameters(); }
        }

       
    }
}