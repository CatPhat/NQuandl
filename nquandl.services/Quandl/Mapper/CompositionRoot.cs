using System.Reflection;
using NQuandl.Api;
using SimpleInjector;

namespace NQuandl.Services.Quandl.Mapper
{
    public static class CompositionRoot
    {
        public static void RegisterQuandlJsonMapper(this Container container, params Assembly[] assemblies)
        {
            assemblies = assemblies ?? new[] {Assembly.GetAssembly(typeof (IMapObjectToEntity<>))};
            container.RegisterCollection(typeof (IMapObjectToEntity<>), assemblies);
        }

        public static void RegisterQuandlCsvMapper(this Container container, params Assembly[] assemblies)
        {
            assemblies = assemblies ?? new[] { Assembly.GetAssembly(typeof(IMapCsvStream)) };
            container.Register(typeof(IMapCsvStream), assemblies);
        }
    }
}