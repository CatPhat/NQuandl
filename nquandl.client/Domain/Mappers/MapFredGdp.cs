using NQuandl.Client.Api;
using NQuandl.Client.Domain.Entities;

namespace NQuandl.Client.Domain.Mappers
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