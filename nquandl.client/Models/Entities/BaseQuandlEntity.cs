using NQuandl.Client.Interfaces;

namespace NQuandl.Client.Entities
{
    public abstract class QuandlEntity : IQuandlEntity
    {
        public abstract string QuandlCode { get; }
    }

    public abstract class QuandlCommodityEntity : QuandlEntity
    {
        public abstract string DatabaseCode { get; }
        public abstract string TableCode { get; }

        public override string QuandlCode
        {
            get { return DatabaseCode + "/" + TableCode; }
        }
    }
}