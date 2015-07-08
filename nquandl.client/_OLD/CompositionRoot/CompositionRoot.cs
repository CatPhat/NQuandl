using NQuandl.Client.Entities.Base;
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
            Container.RegisterManyForOpenGeneric(typeof (IMapJsonToEntity<>), typeof (IMapJsonToEntity<>).Assembly);
            Container.Verify();
        }

        public static IMapJsonToEntity<TEntity> GetMapper<TEntity>() where TEntity : QuandlEntity
        {
            return Container.GetInstance<IMapJsonToEntity<TEntity>>();
        }
    }
}