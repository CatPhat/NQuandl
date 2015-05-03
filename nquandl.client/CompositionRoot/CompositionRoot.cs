using NQuandl.Client.Interfaces;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace NQuandl.Client.CompositionRoot
{
    public static class Bootstapper
    {
        public static Container Container;

        static Bootstapper()
        {
            Container = new Container();

            Container.Register<IMapProcessor, MapProcessor>();
            Container.RegisterManyForOpenGeneric(typeof (IMapData<>), typeof (IMapData<>).Assembly);

            Container.Verify();
        }
    }
}