namespace NQuandl.Api.Quandl
{
    public interface IMapObjectToEntity<TEntity> where TEntity : QuandlEntity
    {
        TEntity MapEntity(object[] dataObject);
    }
}