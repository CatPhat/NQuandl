using NQuandl.Client.Api.Helpers;

namespace NQuandl.Client.Api.Configuration
{
    public class AppConfiguration
    {
        private readonly IReadConfiguration _configuration;

        public AppConfiguration(IReadConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ApiKey
        {
            get
            {
                var apiKey = _configuration.AppSettings[AppSettingKey.ApiKey.ToString()];
                return string.IsNullOrEmpty(apiKey) ? string.Empty : $"{RequestParameterConstants.AuthToken}={apiKey}";
            }
        }

        public string BaseUrl => _configuration.AppSettings[AppSettingKey.BaseUrl.ToString()] ?? @"https://quandl.com";

        public string RootUrl
        {
            get
            {
                var baseUrl = BaseUrl.RemoveTrailingSlash();
                return $"{BaseUrl}/api";
            }
        }
    }
}