using NQuandl.Services.CompositionRoot;
using SimpleInjector;

namespace NQuandl.SimpleClient
{
    public static class Bootstrapper
    {
        public static Container Bootstrap()
        {
            var container = new Container();
            container.ComposeRoot();
           
            container.Verify();

            return container;
        }
    }
}