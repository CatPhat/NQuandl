using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Services.Database;
using NQuandl.Npgsql.Services.Database.Configuration;
using NQuandl.Npgsql.Services.Database.Customization;
using NQuandl.Npgsql.Services.Database.Initialization;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Npgsql.SimpleInjector.Database
{
    public class Package : IPackage
    {
        private bool IsGreenfield { get; }
        public Package(bool isGreenfield = false)
        {
            IsGreenfield = isGreenfield;
        }
     
        public void RegisterServices(Container container)
        {
#if DEBUG
            container.Register<IConfigureConnection>(() => new DebugConnectionConfiguration());
#endif
#if !DEBUG
            container.Register<IConfigureConnection>(() => new ConnectionConfiguration());
#endif

            if (IsGreenfield)
            {
                container.Register<ICustomizeDb, PostgresSqlScriptsCustomizer>();
                container.Register<IDbInitializer, GreenfieldDbInitializer>();
            }
            else
            {
                container.Register<ICustomizeDb, VainillaDbCustomizer>();
                container.Register<IDbInitializer, BrownfrieldDbInitializer>();
            }

            container.Register<IDb, Db>(Lifestyle.Transient);
        }
    }
}