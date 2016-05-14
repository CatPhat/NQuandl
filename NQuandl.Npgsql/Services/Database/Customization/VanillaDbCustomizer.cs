using NQuandl.Npgsql.Api;

namespace NQuandl.Npgsql.Services.Database.Customization
{
    public class VanillaDbCustomizer : ICustomizeDb
    {
        public void Customize(IDbContext dbContext)
        {
            // do not customize
        }
    }
}