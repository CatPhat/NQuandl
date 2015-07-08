using NQuandl.Client.Api;

namespace NQuandl.Client._OLD.Interfaces.old
{
    public interface IMapJsonToEntity<out TEntity> where TEntity : QuandlEntity
    {
        TEntity Map(object[] objects);
    }
}