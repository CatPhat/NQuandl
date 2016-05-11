using Npgsql;

namespace NQuandl.Npgsql.Api
{
    public interface IDb
    {
        NpgsqlConnection CreateConnection();
    }
}