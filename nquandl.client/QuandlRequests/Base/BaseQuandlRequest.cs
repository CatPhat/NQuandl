
using System;

namespace NQuandl.Client
{
    public abstract class QuandlResponse
    {
        
    }

   
    public abstract class BaseQuandlRequest<TResponse> where TResponse : QuandlResponse
    {
        internal static string ResponseFormat
        {
            get { return "json"; }
        }

        public static Type ResponseType
        {
            get { return typeof (TResponse); }
        }

        public abstract string QueryCode { get; }
        public abstract string Parameters { get; }
        public abstract string Url { get;}

    }

    public abstract class BaseQuandlRequestV1<TResponse> : BaseQuandlRequest<TResponse> where TResponse : QuandlResponse
    {
        public override string Url
        {
            get { return QuandlServiceConfiguration.BaseUrl + "v1/datasets/" + QueryCode + "." + ResponseFormat + "?" + Parameters.AppendApiKey(QuandlServiceConfiguration.ApiKey); }
        }
    }

    public abstract class BaseQuandlRequestV2<TResponse> : BaseQuandlRequest<TResponse> where TResponse : QuandlResponse
    {
        public override string Url
        {
            get { return QuandlServiceConfiguration.BaseUrl + "v2/datasets." + ResponseFormat + "?query=*&source_code=" + QueryCode + "&" + Parameters.AppendApiKey(QuandlServiceConfiguration.ApiKey); }
        }
    }


    public abstract class BaseQuandlRequestWithQueryOptions<TResponse> : BaseQuandlRequestV1<TResponse> where TResponse : QuandlResponse
    {
       
        internal abstract string DatabaseCode { get; }
        internal abstract string TableCode { get; }
        
        public SortOrder SortOrder { get; set; }
        public bool ExcludeHeaders { get; set; }

        public override string Parameters
        {
            get { return SortOrder.ToString(); } //needs extension
        }
    }

 
  

}
