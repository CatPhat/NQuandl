using System.Reflection;
using NQuandl.Api;
using NQuandl.Api.Quandl;
using SimpleInjector;

namespace NQuandl.Services.Quandl.Mapper
{
    public static class CompositionRoot
    {
        public static void RegisterQuandlJsonMapper(this Container container, params Assembly[] assemblies)
        {
            assemblies = assemblies ?? new[] {Assembly.GetAssembly(typeof (IMapObjectToEntity<>))};
            container.Register(typeof (IMapObjectToEntity<>), assemblies);
        }

        public static void RegisterQuandlCsvMapper(this Container container)
        {
           
            container.Register<IMapCsvStream, MapCsvStream>();
        }
    }
}