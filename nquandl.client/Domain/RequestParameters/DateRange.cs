using System;

namespace NQuandl.Client.Domain.RequestParameters
{
    public class DateRange
    {
        public DateTime TrimStart { get; set; }
        public DateTime TrimEnd { get; set; }
    }
}