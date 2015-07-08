using NQuandl.Client.Entities.Base;

namespace NQuandl.Client.Requests
{
    public class JsonRequestV1<TEntity> where TEntity : QuandlEntity
    {
        public 
        public PathSegmentParametersV1 PathSegmentParametersV1 { get; set; }

    }
}