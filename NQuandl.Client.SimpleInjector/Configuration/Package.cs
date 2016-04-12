using Microsoft.Framework.Configuration;
using NQuandl.Client.Api.Configuration;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Client.SimpleInjector.Configuration
{
    public class Package : IPackage
    {
        private readonly IConfigurationSection _configuration;
        public Package(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public void RegisterServices(Container container)
        {
            var config = _configuration;
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