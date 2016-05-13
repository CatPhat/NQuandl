using NQuandl.Npgsql.Api;

namespace NQuandl.Npgsql.Services.Database.Customization
{
    public class VanillaDbCustomizer : ICustomizeDb
    {
        public void Customize(IDb db)
        {
            // do not customize
        }
    }
}