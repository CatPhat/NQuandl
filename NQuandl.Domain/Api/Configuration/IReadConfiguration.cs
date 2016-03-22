using System.Collections.Specialized;


namespace NQuandl.Api.Configuration
{
    public interface IReadConfiguration
    {
        NameValueCollection AppSettings { get; }
        //ConfigurationSection GetSection(string sectionName);
    }
}