using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;

namespace NQuandl.Client.URIs
{
    public interface IContainUri
    {
        string Uri { get; }
    }

    public class QuandlVersion1Uri : IContainUri
    {
        private readonly RequiredRequestParameters _required;
        private readonly OptionalRequestParameters _optional;

        public QuandlVersion1Uri(string quandlCode, ResponseFormat format, OptionalRequestParameters optional = null)
        {
            _required = new RequiredRequestParameters
            {
                ApiVersion = RequestParameterConstants.ApiVersion1,
                ResponseFormat = format.GetStringValue(),

                QuandlCode = quandlCode
            };
            _optional = optional;
        }

        public string Uri
        {
            get { return _required.ToUriV1() + _optional.ToQueryUri(); }
        }
    }
}
