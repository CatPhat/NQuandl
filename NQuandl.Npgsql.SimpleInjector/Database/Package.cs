using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Services.Database;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Npgsql.SimpleInjector.Database
{
    public class Package : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IDb, Db>(Lifestyle.Transient);
        }
    }
}