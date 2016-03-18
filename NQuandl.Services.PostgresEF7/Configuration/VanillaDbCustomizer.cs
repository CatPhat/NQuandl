using JetBrains.Annotations;
using Microsoft.Data.Entity;

namespace NQuandl.Services.PostgresEF7.Configuration
{
    [UsedImplicitly]
    public class VanillaDbCustomizer : ICustomizeDb
    {
        public void Customize(DbContext dbContext)
        {
            // do not customize
        }
    }
}