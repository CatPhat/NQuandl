using System.Reflection;
using Microsoft.Framework.Configuration;

namespace NQuandl.Services.CompositionRoot
{
    public class RootCompositionSettings
    {
        public Assembly[] QuandlCsvMapperAssemblies { get; set; }
        public Assembly[] QuandlRequestHandlerAssemblies { get; set; }
        public IConfigurationSection Configuration { get; set; }
    }
}