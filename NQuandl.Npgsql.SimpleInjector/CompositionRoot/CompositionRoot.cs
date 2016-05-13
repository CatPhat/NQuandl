using System;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Npgsql.SimpleInjector.CompositionRoot
{
    public static class CompositionRoot
    {
        public static void ComposeRoot(this Container container, CompositionRootSettings settings)
        {
            settings = settings ?? new CompositionRootSettings();
#if !DEBUG
            settings.IsGreenfield = false;
#endif
            settings.UseDebugDatabase = true;

            container.Register<IServiceProvider>(() => container, Lifestyle.Singleton);

            var packages = new IPackage[]
            {
                new Database.Package(settings.IsGreenfield, settings.UseDebugDatabase),
                new Mapper.Package(),
                new Metadata.Package(settings.MetadataCacheInitializerAssemblies, settings.MetadataCacheAssemblies),
                new Transactions.Commands.Package(settings.CommandHandlerAssemblies),
                new Transactions.Queries.Package(settings.QueryHandlerAssemblies)
            };

            container.RegisterPackages(packages);
        }
    }
}