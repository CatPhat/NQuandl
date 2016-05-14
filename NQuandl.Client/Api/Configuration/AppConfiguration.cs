using JetBrains.Annotations;
using Microsoft.Framework.Configuration;

namespace NQuandl.Client.Api.Configuration
{
    [UsedImplicitly]
    public class AppConfiguration
    {
        private readonly IConfigurationSection _configuration;

        public AppConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public string ApiKey
        {
            get { return _configuration[AppSettingKey.ApiKey.ToString()]; }
        }

        public string BaseUrl
        {
            get { return _configuration[AppSettingKey.BaseUrl.ToString()]  ?? @"https://quandl.com/api"; }
        }
    }
}