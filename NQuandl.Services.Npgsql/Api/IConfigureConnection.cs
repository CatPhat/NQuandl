namespace NQuandl.Services.Npgsql.Api
{
    public interface IConfigureConnection
    {
        string ConnectionString { get; }
    }
}