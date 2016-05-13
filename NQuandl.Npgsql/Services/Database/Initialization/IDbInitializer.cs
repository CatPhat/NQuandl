using NQuandl.Npgsql.Api;

namespace NQuandl.Npgsql.Services.Database.Initialization
{
    public interface IDbInitializer {
        void Intialize(IDb db);
    }
}