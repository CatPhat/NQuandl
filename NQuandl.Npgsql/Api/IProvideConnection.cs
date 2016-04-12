using Npgsql;

namespace NQuandl.Npgsql.Api
{
    public interface IProvideConnection
    {
        //todo return interface to remove Npgsql dependency
        NpgsqlConnection GetConnection();
    }
}