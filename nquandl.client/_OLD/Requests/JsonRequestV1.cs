using NQuandl.Client.Api;
using NQuandl.Client._OLD.Requests.old;

namespace NQuandl.Client._OLD.Requests
{
    public class JsonRequestV1<TEntity> where TEntity : QuandlEntity
    {
        public 
        public PathSegmentParametersV1 PathSegmentParametersV1 { get; set; }

    }
}