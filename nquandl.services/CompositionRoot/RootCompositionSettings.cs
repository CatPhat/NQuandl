using System.Reflection;
using Microsoft.Framework.Configuration;

namespace NQuandl.Services.CompositionRoot
{
    public class RootCompositionSettings
    {
        public Assembly[] QueryHandlerAssemblies { get; set; }
        public Assembly[] QuandlCsvMapperAssemblies { get; set; }
        public Assembly[] CommandHandlerAssemblies { get; set; }
        public Assembly[] QuandlRequestHandlerAssemblies { get; set; }
        public IConfigurationSection Configuration { get; set; }
    }
}