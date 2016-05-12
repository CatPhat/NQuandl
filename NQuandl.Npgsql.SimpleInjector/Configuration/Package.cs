using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Services.Database;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Npgsql.SimpleInjector.Configuration
{
    public class Package : IPackage
    {
        public void RegisterServices(Container container)
        {
#if DEBUG
            container.Register<IConfigureConnection>(() => new DebugConnectionConfiguration(), Lifestyle.Singleton);
#endif
#if !DEBUG
            container.Register<IConfigureConnection>(() => new ConnectionConfiguration(), Lifestyle.Singleton);
#endif
        }
    }
}