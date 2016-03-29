using NQuandl.Api.Quandl;
using SimpleInjector;

namespace NQuandl.Services.Quandl.Mapper
{
    public static class CompositionRoot
    {
        public static void RegisterQuandlCsvMapper(this Container container)
        {
            container.Register<IMapCsvStream, MapCsvStream>();
        }
    }
}