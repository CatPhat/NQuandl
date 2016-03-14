using NQuandl.Api;
using NQuandl.Domain.Entities;

namespace NQuandl.Domain.Mappers
{
    public class MapFredGdp : IMapObjectToEntity<FredGdp>
    {
        public FredGdp MapEntity(object[] dataObject)
        {
            return new FredGdp
            {
                Date = dataObject[0].ToString(),
                Value = double.Parse(dataObject[1].ToString())
            };
        }
    }
}