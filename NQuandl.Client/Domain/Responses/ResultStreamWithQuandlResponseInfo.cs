using System.IO;

namespace NQuandl.Client.Domain.Responses
{
    public class ResultStreamWithQuandlResponseInfo : ResultWithQuandlResponseInfo
    {
        public Stream ContentStream { get; set; }
    }
}