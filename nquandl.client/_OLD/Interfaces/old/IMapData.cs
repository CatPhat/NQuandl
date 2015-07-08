using NQuandl.Client.Entities.Base;

namespace NQuandl.Client.Interfaces
{
    public interface IMapJsonToEntity<out TEntity> where TEntity : QuandlEntity
    {
        TEntity Map(object[] objects);
    }
}