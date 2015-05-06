using NQuandl.Client.Entities.Base;

namespace NQuandl.Client.Interfaces
{
    public interface IMapData<out TEntity> where TEntity : QuandlEntity
    {
        TEntity Map(object[] objects);
    }
}