namespace NQuandl.Client.Api
{
    public abstract class QuandlEntity
    {
        public abstract string DatabaseCode { get; }
        public abstract string DatasetCode { get; }
    }
}