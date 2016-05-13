using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Npgsql.SimpleInjector.CompositionRoot
{
    public static class CompositionRoot
    {
        public static void ComposeRoot(this Container container, CompositionRootSettings settings)
        {
            var packages = new IPackage[]
            {
                new Configuration.Package(),
                new Database.Package(settings.IsGreenfield),
                new Mapper.Package(),
                new Metadata.Package(settings.MetadataCacheInitializerAssemblies, settings.MetadataCacheAssemblies),
                new Transactions.Commands.Package(settings.CommandHandlerAssemblies),
                new Transactions.Queries.Package(settings.QueryHandlerAssemblies)
            };

            container.RegisterPackages(packages);
        }
    }
}