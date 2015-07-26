using System.Collections.Specialized;
using System.Configuration;

namespace NQuandl.Client.Api.Configuration
{
    public interface IReadConfiguration
    {
        NameValueCollection AppSettings { get; }
        ConfigurationSection GetSection(string sectionName);
    }
}