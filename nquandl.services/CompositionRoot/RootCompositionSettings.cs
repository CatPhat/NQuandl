using System.Reflection;

namespace NQuandl.Services.CompositionRoot
{
    public class RootCompositionSettings
    {
        public Assembly[] QueryHandlerAssemblies { get; set; }
        public Assembly[] QuandlJsonMapperAssemblies { get; set; }
        public Assembly[] QuandlCsvMapperAssemblies { get; set; }
    }
}