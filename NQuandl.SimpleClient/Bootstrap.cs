using NQuandl.Services.CompositionRoot;
using SimpleInjector;

namespace NQuandl.SimpleClient
{
    public static class Bootstrapper
    {
        public static void Bootstrap()
        {
            var container = new Container();
            container.ComposeRoot();
           
            container.Verify();
            container.RegisterQueryExtensions();
        }
    }
}