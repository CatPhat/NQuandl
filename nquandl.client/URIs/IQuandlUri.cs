using System.Collections.Generic;
using NQuandl.Client.Requests;

namespace NQuandl.Client.URIs
{
    public interface IQuandlUri
    {
        string PathSegment { get; }
        Dictionary<string,string> QueryParmeters { get; }
    }
}