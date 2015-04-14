using System;
using System.Configuration;

namespace NQuandl.Client
{
    public enum QuandlServiceSettingsKey
    {
        BaseUrl,
        ApiKey
    }
    
    public static class QuandlServiceConfiguration
    {
        public static string BaseUrl
        {
            get { return ConfigurationManager.AppSettings[QuandlServiceSettingsKey.BaseUrl.ToString()]; }
        }

        public static string ApiKey
        {
            get { return ConfigurationManager.AppSettings[QuandlServiceSettingsKey.ApiKey.ToString()]; }
        }

    }
}
