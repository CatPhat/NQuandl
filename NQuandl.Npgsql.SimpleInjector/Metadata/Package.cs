using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NQuandl.Npgsql.Api.Metadata;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Npgsql.SimpleInjector.Metadata
{
    public class Package : IPackage
    {
        public Package(Assembly[] entityMetadataCacheInitializerAssemblies = null,
            params Assembly[] entityMetadataCacheAssemblies)
        {
            if (entityMetadataCacheInitializerAssemblies == null || !entityMetadataCacheInitializerAssemblies.Any())
            {
                entityMetadataCacheInitializerAssemblies = new[] {typeof(IEntityMetadataCacheInitializer<>).Assembly};
            }
            
            if (entityMetadataCacheAssemblies == null || !entityMetadataCacheAssemblies.Any())
            {
                entityMetadataCacheAssemblies = new[] {typeof(IEntityMetadataCache<>).Assembly};
            }

            EntityMetadataCacheInitializerAssemblies = entityMetadataCacheInitializerAssemblies;
            EntityMetadataCacheAssemblies = entityMetadataCacheAssemblies;
        }

        private IEnumerable<Assembly> EntityMetadataCacheInitializerAssemblies { get; }
        private IEnumerable<Assembly> EntityMetadataCacheAssemblies { get; }

        public void RegisterServices(Container container)
        {
            container.Register(typeof(IEntityMetadataCacheInitializer<>), EntityMetadataCacheInitializerAssemblies, Lifestyle.Singleton);
            container.Register(typeof(IEntityMetadataCache<>), EntityMetadataCacheAssemblies, Lifestyle.Singleton);
        }
    }
}