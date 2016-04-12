namespace NQuandl.Npgsql.Api
{
    public interface IConfigureConnection
    {
        string ConnectionString { get; }
    }
}