using NQuandl.Npgsql.Api;

namespace NQuandl.Npgsql.Services.Database.Customization
{
    public interface ICustomizeDb
    {
        void Customize(IDb db);
    }
}