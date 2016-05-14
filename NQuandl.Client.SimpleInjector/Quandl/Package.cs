using NQuandl.Client.Api.Quandl;
using NQuandl.Client.Services.Quandl;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Client.SimpleInjector.Quandl
{
    public class Package : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IQuandlClient, QuandlClient>();
        }
    }
}