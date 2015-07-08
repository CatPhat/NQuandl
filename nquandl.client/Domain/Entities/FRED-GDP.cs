using NQuandl.Client.Entities.Base;

namespace NQuandl.Client.Domain.Entities
{
    public class FredGdp : QuandlEntity
    {
        public string Date { get; set; }
        public double Value { get; set; }

        public static class Constants
        {
            public const string DatabaseCode = "FRED";
            public const string TableCode = "GDP";
            public static readonly string QuandlCode = string.Format("{0}/{1}", DatabaseCode, TableCode);
        }
    }
}