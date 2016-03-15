namespace NQuandl.Api
{
    public abstract class QuandlEntity
    {
        public abstract string DatabaseCode { get; }
        public abstract string DatasetCode { get; }
    }
}