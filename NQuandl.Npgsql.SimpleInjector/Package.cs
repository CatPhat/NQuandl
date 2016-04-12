using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Services;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Npgsql.SimpleInjector
{
    public class Package : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.RegisterSingleton<IConfigureConnection, PostgresConnection>();
            container.RegisterSingleton<IProvideConnection, ConnectionProvider>();
            container.RegisterSingleton<IConstructConnection, ConnectionFactory>();
            container.Register<IExecuteRawSql, ExecuteRawSql>();
        }
    }
}