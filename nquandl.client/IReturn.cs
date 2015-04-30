using System;
using System.Collections;
using NQuandl.Client.Models;

namespace NQuandl.Client
{
    public interface IReturn
    {
      
    }

    public interface IReturn<TResponse> : IReturn where TResponse : QuandlResponse
    {
       
    }
}
