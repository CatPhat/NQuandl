using NQuandl.Npgsql.Api.Mappers;
using NQuandl.Npgsql.Services.Mappers;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Npgsql.SimpleInjector.Mapper
{
    public class Package : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ISqlMapper, SqlMapper>(Lifestyle.Transient);
        }
    }
}