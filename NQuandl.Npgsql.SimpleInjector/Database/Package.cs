using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Services.Database;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Npgsql.SimpleInjector.Database
{
    public class Package : IPackage
    {
        private bool IsGreenfield { get; }
        public Package(bool isGreenfield)
        {
            IsGreenfield = isGreenfield;
        }

        //todo if greenfield set intializer
        public void RegisterServices(Container container)
        {
            container.Register<IDb, Db>(Lifestyle.Transient);
        }
    }
}