
using NQuandl.Client.Helpers;

namespace NQuandl.Client
{
    public abstract class QuandlResponse
    {

    }


    public interface IQuandlRequest<TResponse> where TResponse : QuandlResponse
    {
        //internal static string ResponseFormat
        //{
        //    get { return "json"; }
        //}
        string Url { get; }
    }

    public abstract class BaseQuandlRequestV1<TResponse> : IQuandlRequest<TResponse> where TResponse : QuandlResponse
    {
        protected BaseQuandlRequestV1(QuandlCode quandlCode, OptionalRequestParameters options = null)
        {
            _quandlCode = quandlCode;
            if (options != null)
            {
                _options = options.ToRequestParameter();
            }
        }

        private readonly string _options;
        private readonly QuandlCode _quandlCode;
        public string Url
        {
            get
            {
                return QuandlServiceConfiguration.BaseUrl + RequestParameterConstants.Version1Format + "/" +
                       _quandlCode.DatabaseCode + _quandlCode.TableCode + RequestParameterConstants.JsonFormat +
                       _options + "&" +
                       RequestParameter.ApiKey(QuandlServiceConfiguration.ApiKey);
            }
        }
    }

    public abstract class BaseQuandlRequestV2<TResponse> : IQuandlRequest<TResponse> where TResponse : QuandlResponse
    {
        public string QueryCode { get; set; }

        public string Url
        {
            get { return QuandlServiceConfiguration.BaseUrl + RequestParameterConstants.Version2Format + "." + RequestParameterConstants.JsonFormat + "?query=*&source_code=" + QueryCode + "&" + RequestParameter.ApiKey(QuandlServiceConfiguration.ApiKey); }
        }
    }

}
