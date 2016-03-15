using JetBrains.Annotations;

namespace NQuandl.Api.Configuration
{
    [UsedImplicitly]
    public class AppConfiguration
    {
        private readonly IReadConfiguration _configuration;

        public AppConfiguration(IReadConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ApiKey
        {
            get { return _configuration.AppSettings[AppSettingKey.ApiKey.ToString()]; }
        }

        public string BaseUrl
        {
            get { return _configuration.AppSettings[AppSettingKey.BaseUrl.ToString()] ?? @"https://quandl.com/api"; }
        }
    }
}