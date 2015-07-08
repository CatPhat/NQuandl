using System;
using System.Configuration;
using System.Data.Odbc;
using System.Reflection;
using System.Threading;
using NQuandl.Client.Api;
using NQuandl.Client.Domain;
using NQuandl.Client.Domain.Queries;
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

            _container.RegisterMapper();
            _container.RegisterQueries();
            _container.RegisterQuandlClient();

            _container.Verify();
        }

        public static IProcessQueries GetQueryProcessor()
        {
           return _container.GetInstance<IProcessQueries>();
        }

        private static void RegisterQuandlClient(this Container container)
        {
            //container.Register<IQuandlRestClient>(() => new QuandlRestClient("http://localhost:49832/api"));
            container.Register<IQuandlRestClient>(() => new QuandlRestClient("https://quandl.com/api"));

            container.Register<IQuandlClient>(() => new QuandlClient(container.GetInstance<IQuandlRestClient>()));
            container.Register<IQuandlJsonClient>(() => new QuandlJsonClient(container.GetInstance<IQuandlClient>(), GetQueryProcessor()));
        }

        private static void RegisterMapper(this Container container)
        {
            container.RegisterManyForOpenGeneric(typeof(IMapObjectToEntity<>), typeof(IMapObjectToEntity<>).Assembly);
        }

        private static void RegisterQueries(this Container container, params Assembly[] assemblies)
        {
          
            container.RegisterSingle<IProcessQueries, QueryProcessor>();

            var assembly = typeof (IHandleQuery<,>).Assembly;
           
            container.RegisterManyForOpenGeneric(typeof(IHandleQuery<,>), assembly);
            container.RegisterOpenGeneric(typeof(IHandleQuery<,>), typeof(HandleGetQuandlCodeByEntity<>));
            container.RegisterOpenGeneric(typeof(IHandleQuery<,>), typeof(HandleDeserializeToClass<>));
            container.RegisterOpenGeneric(typeof(IHandleQuery<,>), typeof(HandleDeserializeToJsonResponseV1<>));
            container.RegisterOpenGeneric(typeof(IHandleQuery<,>), typeof(HandleMapToEntitiesByDataObjects<>));
            container.RegisterOpenGeneric(typeof(IHandleQuery<,>), typeof(HandleRequestJsonResponseV1ByEntity<>));
         
        }
    }
 
}