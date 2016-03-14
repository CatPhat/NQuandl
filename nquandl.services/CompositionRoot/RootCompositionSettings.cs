using System.Reflection;

namespace NQuandl.Services.CompositionRoot
{
    public class RootCompositionSettings
    {
        public Assembly[] QueryHandlerAssemblies { get; set; }
        public Assembly[] QuandlMapperAssemblies { get; set; }
    }
}