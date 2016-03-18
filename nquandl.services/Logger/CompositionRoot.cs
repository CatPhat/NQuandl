using SimpleInjector;

namespace NQuandl.Services.Logger
{
    public static class CompositionRoot
    {
        public static void RegisterLogger(this Container container)
        {
            container.RegisterSingleton<ILogger, Logger>();
        }
    }
}