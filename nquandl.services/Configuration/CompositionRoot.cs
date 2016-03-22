using Microsoft.Framework.Configuration;

using NQuandl.Api.Configuration;
using SimpleInjector;

namespace NQuandl.Services.Configuration
{
    public static class CompositionRoot
    {
        public static void RegisterConfiguration(this Container container, IConfigurationSection configuration)
        {
            container.RegisterSingleton<IConfigurationSection>(configuration);
            container.RegisterSingleton<AppConfiguration>();
        }
    }
}