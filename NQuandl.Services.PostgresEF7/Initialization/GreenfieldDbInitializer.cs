using JetBrains.Annotations;
using Microsoft.Data.Entity;
using NQuandl.Services.PostgresEF7.Configuration;

namespace NQuandl.Services.PostgresEF7.Initialization
{
    [UsedImplicitly]
    public class GreenfieldDbInitializer : IDatabaseInitializer<DbContext>
    {
        private readonly ICustomizeDb _customizer;

        public GreenfieldDbInitializer(ICustomizeDb customizer)
        {
            _customizer = customizer;
        }
        
        public void InitializeDatabase(DbContext context)
        {
            _customizer?.Customize(context);
        }
    }
}