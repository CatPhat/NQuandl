using Npgsql;

namespace NQuandl.Npgsql.Api
{
    public interface IProvideDbConnection
    {
        NpgsqlConnection CreateConnection();
    }
}