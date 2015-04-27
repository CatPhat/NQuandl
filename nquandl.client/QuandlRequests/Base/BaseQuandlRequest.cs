
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
        RequiredRequestParameters RequiredRequestParameters { get; set; }
        OptionalRequestParameters OptionalRequestParameters { get; set; }

       string QueryCode { get; }
       string Url { get;}
    }

    public abstract class BaseQuandlRequestV1<TResponse> : IQuandlRequest<TResponse> where TResponse : QuandlResponse
    {
        public RequiredRequestParameters RequiredRequestParameters { get; set; }
        public OptionalRequestParameters OptionalRequestParameters { get; set; }

        public string QueryCode { get; private set; }

        public string Url
        {
            get { return QuandlServiceConfiguration.BaseUrl + RequestParameterConstants.Version1Format + QueryCode + RequestParameterConstants.JsonFormat + OptionalRequestParameters.ToRequestParameter(); }
        }
    }

    public abstract class BaseQuandlRequestV2<TResponse> : IQuandlRequest<TResponse> where TResponse : QuandlResponse
    {
        public RequiredRequestParameters RequiredRequestParameters { get; set; }
        OptionalRequestParameters IQuandlRequest<TResponse>.OptionalRequestParameters { get; set; }
        public string OptionalRequestParameters { get; set; }
        public string QueryCode { get; private set; }

        public override string Url
        {
            get { return QuandlServiceConfiguration.BaseUrl + "v2/datasets." + ResponseFormat + "?query=*&source_code=" + QueryCode + "&" + RequestParameter.ApiKey(QuandlServiceConfiguration.ApiKey); }
        }
    }
    
}
