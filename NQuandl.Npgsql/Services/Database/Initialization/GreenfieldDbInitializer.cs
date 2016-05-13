using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Services.Database.Customization;

namespace NQuandl.Npgsql.Services.Database.Initialization
{
    public class GreenfieldDbInitializer : IDbInitializer
    {
        private readonly ICustomizeDb _customizer;

        public GreenfieldDbInitializer(ICustomizeDb customizer)
        {
            _customizer = customizer;
        }

        public void Intialize(IDb db)
        {
            _customizer?.Customize(db);
        }
    }
}