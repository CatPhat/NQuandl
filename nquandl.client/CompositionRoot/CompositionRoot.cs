using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;
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
            Container.RegisterManyForOpenGeneric(typeof(INQuandlRequest<>), typeof(NQuandlRequest<>).Assembly);
            Container.Register<IGetNQuandlRequest, RequestProcessor>();
            Container.Verify();
        }
    }
}