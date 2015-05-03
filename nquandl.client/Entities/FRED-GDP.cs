using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;
using NQuandl.Client.Responses;

namespace NQuandl.Client.Entities
{
    public class FRED_GDP : QuandlCommodityEntity
    {
        public string Date { get; set; }
        public double Value { get; set; }

        public override string DatabaseCode
        {
            get { return "FRED"; }
        }

        public override string TableCode
        {
            get { return "GDP"; }
        }
    }

    public class Request_FRED_GDP : NQuandlRequest<FRED_GDP, QuandlResponseV1>
    {
        public override IMapData<FRED_GDP> Mapper
        {
            get { return new Map_FRED_GDP(); }
        }

        public override IReturn<QuandlResponseV1> QuandlRequest
        {
            get { return new QuandlRequestV1(Parameters); }
        }
    }

    public class Map_FRED_GDP : IMapData<FRED_GDP>
    {
        public FRED_GDP Map(object[] objects)
        {
            return new FRED_GDP
            {
                Date = objects[0].ToString(),
                Value = double.Parse(objects[1].ToString())
            };
        }
    }

  
}