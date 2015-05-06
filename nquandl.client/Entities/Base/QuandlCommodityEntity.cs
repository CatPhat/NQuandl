namespace NQuandl.Client.Entities.Base
{
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