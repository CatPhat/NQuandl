using NQuandl.Client.Api;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace NQuandl.Client.CompositionRoot
{
    public static class Bootstapper
    {
        private static readonly Container _container;

        static Bootstapper()
        {
            _container = new Container();
            _container.RegisterManyForOpenGeneric(typeof(IMapObjectToEntity<>), typeof(IMapObjectToEntity<>).Assembly);
            _container.Verify();
        }
    }
}