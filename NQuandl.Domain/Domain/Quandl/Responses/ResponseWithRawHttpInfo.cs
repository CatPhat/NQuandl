using System.Collections.Generic;
using System.IO;

namespace NQuandl.Domain.Quandl.Responses
{
    public abstract class ResultWithQuandlResponseInfo
    {
        public QuandlClientResponseInfo QuandlClientResponseInfo { get; set; }
    }

    public class ResultStringWithQuandlResponseInfo : ResultWithQuandlResponseInfo
    {
        public string ContentString { get; set; }
    }

    public class ResultStreamWithQuandlResponseInfo : ResultWithQuandlResponseInfo
    {
        public Stream ContentStream { get; set; }
    }

    public class ResultEntityWithQuandlResponseInfo<TResult> : ResultWithQuandlResponseInfo
        where TResult : ResultWithQuandlResponseInfo
    {
        public TResult Result { get; set; }
    }
}