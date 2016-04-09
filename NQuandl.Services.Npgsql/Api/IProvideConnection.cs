using Npgsql;

namespace NQuandl.Services.Npgsql.Api
{
    public interface IProvideConnection
    {
        //todo return interface to remove Npgsql dependency
        NpgsqlConnection GetConnection();
    }
}