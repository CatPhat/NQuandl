using NQuandl.Client.Entities;
using NQuandl.Client.Interfaces;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace NQuandl.Client.CompositionRoot
{
    public static class Bootstapper
    {
        private static readonly Container Container;

        static Bootstapper()
        {
            Container = new Container();
            Container.RegisterManyForOpenGeneric(typeof (IMapData<>), typeof (IMapData<>).Assembly);
            Container.Verify();
        }

        public static IMapData<TEntity> GetMapper<TEntity>() where TEntity : QuandlEntity
        {
           return Container.GetInstance<IMapData<TEntity>>();
        }
    }
}