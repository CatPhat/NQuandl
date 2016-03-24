using Microsoft.Framework.Configuration;

using NQuandl.Api.Configuration;
using SimpleInjector;

namespace NQuandl.Services.Configuration
{
    public static class CompositionRoot
    {
        public static void RegisterConfiguration(this Container container, IConfigurationSection configuration)
        {
            var config = configuration;
            if (config == null)
            {
                var builder = new ConfigurationBuilder();
                config = builder.Build().GetSection("AppSettings");
            }
            container.RegisterSingleton<IConfigurationSection>(config);
            container.RegisterSingleton<AppConfiguration>();
        }
    }
}