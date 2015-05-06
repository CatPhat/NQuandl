using NQuandl.Client.Entities.Base;
using NQuandl.Client.Interfaces;

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