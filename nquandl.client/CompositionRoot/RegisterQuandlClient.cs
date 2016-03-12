using NQuandl.Client.Api;
using NQuandl.Client.Domain;
using NQuandl.Client.Domain.Queries;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace NQuandl.Client.CompositionRoot
{
    public static class ContainerRegistrations
    {
        public static void RegisterHttpClient(this Container container, string baseUrl)
        {
            container.Register<IHttpClient>(() => new HttpClient(baseUrl));
        }

        public static void RegisterQuandlRestClient(this Container container, string apiKey = null)
        {
#if DEBUG
            //http://localhost:49832/api
            container.Register<IQuandlClient>(
                () => new QuandlClient(container.GetInstance<IHttpClient>(), apiKey));
#else
    //https://quandl.com/api
            container.Register<IQuandlRestClient>(() => new QuandlRestClient(container.GetInstance<IHttpClient>()));
#endif
        }


        //public static void RegisterQuandlClient(this Container container)
        //{
        //    container.Register<IQuandlClient>(() => new QuandlClient(container.GetInstance<IQuandlRestClient>()));
        //}


        public static void RegisterMapper(this Container container)
        {
            container.RegisterCollection(typeof (IMapObjectToEntity<>), typeof (IMapObjectToEntity<>).Assembly);
        }

        //todo: openGeneric registrations can probably be consolidated to a single simple injector api call
        public static void RegisterQueries(this Container container)
        {
            container.Register<IProcessQueries, QueryProcessor>();

            var assembly = typeof (IHandleQuery<,>).Assembly;

            container.RegisterCollection(typeof (IHandleQuery<,>), assembly);


            //container.RegisterCollection(typeof (IHandleQuery<,>), typeof (HandleDatasetBy<>).Assembly);
            //container.RegisterCollection(typeof(IHandleQuery<,>), typeof(HandleQuandlQueryBy<>));
        }
    }
}