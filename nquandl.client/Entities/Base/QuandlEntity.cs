using NQuandl.Client.Interfaces;

namespace NQuandl.Client.Entities.Base
{
    public abstract class QuandlEntity : IQuandlEntity
    {
        public abstract string QuandlCode { get; }
    }
}