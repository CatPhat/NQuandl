using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client.Requests
{
    
    //public abstract class BaseQuandlRequestV2<TResponse> : IQuandlRequest<TResponse> where TResponse : QuandlResponse
    //{
    //    public string QueryCode { get; set; }

    //    public string Url
    //    {
    //        get { return QuandlServiceConfiguration.BaseUrl + RequestParameterConstants.Version2Format + "." + RequestParameterConstants.JsonFormat + "?query=*&source_code=" + QueryCode + "&" + RequestParameter.ApiKey(QuandlServiceConfiguration.ApiKey); }
    //    }
    //}

    ////example query: https://www.quandl.com/api/v2/datasets.json?query=*&source_code=FAO&per_page=20&page=1&auth_token=7f4NyH-Qq7cD3EoRpYSs
    //public class QuandlMasterDatasetV2MetadataRequest : BaseQuandlRequestV2<ResponseWithV2Metadata<>
    //{
    //    public QuandlMasterDatasetV2MetadataRequest(string sourceCode, int perPageCount, int currentPage)
    //    {
    //        _sourceCode = sourceCode;
    //        _perPageCount = perPageCount;
    //        _currentPage = currentPage;
    //    }

    //    private readonly string _sourceCode;
    //    private readonly int _perPageCount;
    //    private readonly int _currentPage;
        
    //    public string OptionalRequestParameter
    //    {
    //        get { return "per_page=" + _perPageCount + "&current_page=" + _currentPage; }
    //    }
    //}

    

   

   
}
