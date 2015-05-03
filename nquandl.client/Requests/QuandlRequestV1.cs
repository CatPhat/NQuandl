using NQuandl.Client.Helpers;
using NQuandl.Client.Interfaces;

namespace NQuandl.Client.Requests
{
    public class QuandlRequestV1 : IQuandlRequest
    {
        private readonly RequestParameters _parameters;

        public QuandlRequestV1(RequestParameters parameters)
        {
            _parameters = parameters;
        }

        public string Url
        {
            get
            {
                var url = QuandlServiceConfiguration.BaseUrl + "/" + RequestParameterConstants.Version1Format + "/" +
                             _parameters.QuandlCode + RequestParameterConstants.JsonFormat + "?" +
                             RequestParameter.ApiKey(QuandlServiceConfiguration.ApiKey);

                if (_parameters.Options == null)
                {
                    return url;
                }
                return url + _parameters.Options.ToRequestParameter();
            }
        }
    }
}