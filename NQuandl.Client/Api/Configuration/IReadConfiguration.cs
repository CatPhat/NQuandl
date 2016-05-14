using System.Collections.Specialized;

namespace NQuandl.Client.Api.Configuration
{
    public interface IReadConfiguration
    {
        NameValueCollection AppSettings { get; }
        //ConfigurationSection GetSection(string sectionName);
    }
}