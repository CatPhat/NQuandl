namespace NQuandl.Client.Api
{
    public interface IMapObjectToEntity<TEntity> where TEntity : QuandlEntity
    {
        TEntity MapEntity(object[] dataObject);
    }
}