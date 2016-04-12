using NQuandl.Client.Services.Logger;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Client.SimpleInjector.Logger
{
    public class Package : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.RegisterSingleton<ILogger, Services.Logger.Logger>();
        }
    }
}