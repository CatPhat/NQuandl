using System.Reflection;
using NQuandl.Api;
using SimpleInjector;

namespace NQuandl.Services.Quandl.Mapper
{
    public static class CompositionRoot
    {
        public static void RegisterQuandlMapper(this Container container, params Assembly[] assemblies)
        {
            assemblies = assemblies ?? new[] {Assembly.GetAssembly(typeof (IMapObjectToEntity<>))};
            container.RegisterCollection(typeof (IMapObjectToEntity<>), assemblies);
        }
    }
}