using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Services;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Npgsql.SimpleInjector
{
    public class Package : IPackage
    {
        //todo move configuration to Package constuctor
        public void RegisterServices(Container container)
        {
            container.RegisterSingleton<IConfigureConnection, PostgresConnection>();
            container.Register<IExecuteRawSql, ExecuteRawSql>();
            

        }
    }
}