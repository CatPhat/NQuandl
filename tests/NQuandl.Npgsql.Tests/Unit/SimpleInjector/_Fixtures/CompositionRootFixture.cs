using System.Reflection;
using NQuandl.Npgsql.SimpleInjector.CompositionRoot;
using SimpleInjector;

namespace NQuandl.Npgsql.Tests.Unit.SimpleInjector._Fixtures
{
    public class CompositionRootFixture
    {
        public CompositionRootFixture()
        {
            Container = new Container();
            var assemblies = new[] {Assembly.GetExecutingAssembly()};
            var settings = new CompositionRootSettings
            {
                IsGreenfield = true,
                MetadataCacheInitializerAssemblies = assemblies,
                MetadataCacheAssemblies = assemblies,
                QueryHandlerAssemblies = assemblies,
                CommandHandlerAssemblies = assemblies
            };
            Container.ComposeRoot(settings);
        }

        public Container Container { get; }
    }
}