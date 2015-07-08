using NQuandl.Client.Api;

namespace NQuandl.Client.Domain.Entities
{
    public class FredGdp : QuandlEntity
    {
        public string Date { get; set; }
        public double Value { get; set; }

        public override string QuandlCode
        {
            get { return string.Format("{0}/{1}", Constants.DatabaseCode, Constants.TableCode); }
        }

        public static class Constants
        {
            public const string DatabaseCode = "FRED";
            public const string TableCode = "GDP";
        }
    }
}