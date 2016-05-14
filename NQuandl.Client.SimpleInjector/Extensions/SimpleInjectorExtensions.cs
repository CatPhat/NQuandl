using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Client.SimpleInjector.Extensions
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