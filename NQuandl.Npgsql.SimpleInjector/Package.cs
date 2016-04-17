using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NQuandl.Client.Api.Transactions;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Services;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Npgsql.SimpleInjector
{
    public class Package : IPackage
    {
        public Package(params Assembly[] mapperAssemblies)
        {
            if (mapperAssemblies == null || !mapperAssemblies.Any())
            {
                mapperAssemblies = new[] { typeof(IMapDataRecordToEntity<>).Assembly };
            }
            MapperAssemblies = mapperAssemblies;
        }

        private IEnumerable<Assembly> MapperAssemblies { get; }

        //todo move configuration to Package constuctor
        public void RegisterServices(Container container)
        {
            container.RegisterSingleton<IConfigureConnection, PostgresConnection>();
            container.Register<IExecuteRawSql, ExecuteRawSql>();
            
            container.Register(typeof(IMapDataRecordToEntity<>), MapperAssemblies);

        }
    }
}