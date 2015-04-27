
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
       string Url { get;}
    }

    public abstract class BaseQuandlRequestV1<TResponse> : IQuandlRequest<TResponse> where TResponse : QuandlResponse
    {
        protected BaseQuandlRequestV1(QuandlCode quandlCode)
        {
            _quandlCode = quandlCode;
        } 
    
        public OptionalRequestParameters OptionalRequestParameters { get; set; }
        private readonly QuandlCode _quandlCode;
        public string Url
        {
            get { return QuandlServiceConfiguration.BaseUrl + RequestParameterConstants.Version1Format + "/" + _quandlCode.DatabaseCode + _quandlCode.TableCode + RequestParameterConstants.JsonFormat + OptionalRequestParameters.ToRequestParameter(); }
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
