using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Npgsql.SimpleInjector.CompositionRoot
{
    public static class SimpleInjectorExtensions
    {
        public static void RegisterPackages(this Container container, params IPackage[] packages)
        {
            foreach (var package in packages)
            {
                package.RegisterServices(container);
            }
        }
    }
}