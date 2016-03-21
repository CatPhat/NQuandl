﻿using NQuandl.Api;
using NQuandl.Api.Quandl;

namespace NQuandl.Domain.Quandl.Entities
{
    public class FredGdp : QuandlEntity
    {
        public string Date { get; set; }
        public double Value { get; set; }

        
        public override string DatabaseCode => "FRED";
        public override string DatasetCode => "GDP";
    }
}