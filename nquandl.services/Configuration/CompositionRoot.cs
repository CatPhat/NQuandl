using NQuandl.Api.Configuration;
using SimpleInjector;

namespace NQuandl.Services.Configuration
{
    public static class CompositionRoot
    {
        public static void RegisterConfiguration(this Container container)
        {
            container.RegisterSingleton<IReadConfiguration, ConfigurationManagerReader>();
            container.RegisterSingleton<AppConfiguration>();
        }
    }
}