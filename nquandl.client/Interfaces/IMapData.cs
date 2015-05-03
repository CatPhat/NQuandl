using NQuandl.Client.Entities;

namespace NQuandl.Client.Interfaces
{
    public interface IMapData<out TEntity> where TEntity : QuandlEntity
    {
        // never change name or dynamic processor will fail
        TEntity Map(object[] objects);
    }
}