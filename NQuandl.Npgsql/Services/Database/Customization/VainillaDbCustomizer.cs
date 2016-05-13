using NQuandl.Npgsql.Api;

namespace NQuandl.Npgsql.Services.Database.Customization
{
    public class VainillaDbCustomizer : ICustomizeDb
    {
        public void Customize(IDb db)
        {
            // do not customize
        }
    }
}