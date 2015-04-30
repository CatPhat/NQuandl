
using NQuandl.Client.Helpers;
using NQuandl.Client.Models;

namespace NQuandl.Client
{

    public interface IQuandlRequest<TResponse> where TResponse : QuandlResponse
    {
        string Url { get; }
    }

    public abstract class BaseQuandlRequestV1<TResponse> : IQuandlRequest<TResponse> where TResponse : QuandlResponse
    {
        private readonly RequestParameters _parameters;
        protected BaseQuandlRequestV1(RequestParameters parameters)
        {
            _parameters = parameters;
        }

        public string Url
        {
            get
            {
                var url = QuandlServiceConfiguration.BaseUrl + "/" + RequestParameterConstants.Version1Format + "/" +
                          _parameters.QuandlCode.DatabaseCode + _parameters.QuandlCode.TableCode + RequestParameterConstants.JsonFormat + "?" +
                          RequestParameter.ApiKey(QuandlServiceConfiguration.ApiKey);

                if (_parameters.Options == null)
                {
                    return url;
                }
                return url + _parameters.Options.ToRequestParameter();
            }
        }
    }

    public class QuandlRequestV1<TResponse> : BaseQuandlRequestV1<TResponse> where TResponse : QuandlResponse
    {

        public QuandlRequestV1(TResponse response, OptionalRequestParameters options = null)
            : base(new RequestParameters { QuandlCode = response.QuandlCode, Options = options })
        {
        }
    }

  






}
