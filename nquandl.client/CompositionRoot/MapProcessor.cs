using System.Diagnostics;
using NQuandl.Client.Entities;
using NQuandl.Client.Interfaces;
using SimpleInjector;

namespace NQuandl.Client.CompositionRoot
{
    internal sealed class MapProcessor : IMapProcessor
    {
        private readonly Container _container;

        public MapProcessor(Container container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public TEntity Process<TEntity>(object[] objects) where TEntity : QuandlEntity
        {
            var handlerType = typeof (IMapData<>).MakeGenericType(typeof (TEntity));

            dynamic handler = _container.GetInstance(handlerType);

            return handler.Map((dynamic) objects); // never change name of Map method
        }
    }

    public interface IMapProcessor
    {
        TResult Process<TResult>(object[] objects) where TResult : QuandlEntity;
    }
}