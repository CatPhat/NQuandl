using System.Reflection;

namespace NQuandl.Npgsql.SimpleInjector.CompositionRoot
{
    public class CompositionRootSettings
    {
        public bool IsGreenfield { get; set; }
        public Assembly[] CommandHandlerAssemblies { get; set; }
        public Assembly[] QueryHandlerAssemblies { get; set; }
        public Assembly[] MetadataCacheInitializerAssemblies { get; set; }
        public Assembly[] MetadataCacheAssemblies { get; set; }
    }
}