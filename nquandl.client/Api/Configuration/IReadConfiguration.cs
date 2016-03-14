using System.Collections.Specialized;
using System.Configuration;

namespace NQuandl.Api.Configuration
{
    public interface IReadConfiguration
    {
        NameValueCollection AppSettings { get; }
        ConfigurationSection GetSection(string sectionName);
    }
}