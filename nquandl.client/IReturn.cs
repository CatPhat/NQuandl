using System;
using System.Collections;

namespace NQuandl.Client
{
    public interface IReturn
    {
      
    }

    public interface IReturn<TResponse> : IReturn where TResponse : QuandlResponse
    {
       
    }
}
