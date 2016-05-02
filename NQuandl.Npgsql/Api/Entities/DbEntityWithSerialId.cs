namespace NQuandl.Npgsql.Api.Entities
{
    public abstract class DbEntityWithSerialId
    {
        public abstract int Id { get; set; }
    }
}