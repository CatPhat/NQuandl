using System.Collections.Generic;
using NQuandl.Client.Requests;

namespace NQuandl.Client.Interfaces
{
    public interface IContainUri
    {
        string PathSegment { get; }
        IEnumerable<QueryParameter> QueryParmeters { get; }
    }
}