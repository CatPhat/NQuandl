using System.IO;

namespace NQuandl.Domain.Quandl.Responses
{
    public class ResultStreamWithQuandlResponseInfo : ResultWithQuandlResponseInfo
    {
        public Stream ContentStream { get; set; }
    }
}