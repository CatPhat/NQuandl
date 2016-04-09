using Npgsql;

namespace NQuandl.Services.Npgsql.Api
{
    public interface IConstructConnection
    {
        NpgsqlConnection ConstructConnection();
    }
}