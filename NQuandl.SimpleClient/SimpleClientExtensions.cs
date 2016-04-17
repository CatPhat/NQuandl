using System.Threading.Tasks;
using NQuandl.Client.Api.Quandl;
using NQuandl.Client.Api.Transactions;
using NQuandl.Client.Domain.Responses;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Transactions;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace NQuandl.SimpleClient
{
    public static class SimpleClientExtensions
    {
        private static readonly Container Container;

        static SimpleClientExtensions()
        {
            Container = new Bootstrapper().Container;
        }

        public static IQuandlClient GetQuandlClient()
        {
            return Container.GetInstance<IQuandlClient>();
        }

        public static async Task ExecuteCommand<TCommand>(this TCommand command) where TCommand : IDefineCommand
        {
            await new ExecuteCommand(Container).Execute(command);
        }

        //public static async Task SaveChangesAsync()
        //{
        //    await new ExecuteCommand(Container).SaveChangesAsync();
        //}

        public static IExecuteRawSql GetSql()
        {
            return Container.GetInstance<IExecuteRawSql>();
        }

        public static TResult ExecuteQuery<TResult>(this IDefineQuery<TResult> query)
        {
            using (Container.BeginExecutionContextScope())
            {
                var queries = Container.GetInstance<IExecuteQueries>();
                var result = queries.Execute(query);
                return result;
            }
        }


        //public static IReadEntities GetReadEntities()
        //{
        //    return Container.GetInstance<IReadEntities>();
        //}

        public static TResult ExecuteRequest<TResult>(this IDefineQuandlRequest<TResult> quandlRequest)
        {
            return new ExecuteQuery(Container).Execute(quandlRequest);
        }

        public static async Task<ResultStringWithQuandlResponseInfo> GetString<TResult>(
            this BaseQuandlRequest<TResult> request)
        {
            var client = Container.GetInstance<IQuandlClient>();
            return await client.GetStringAsync(request.ToUri());
        }
    }

    public class ExecuteCommand
    {
        private readonly Container _container;

        public ExecuteCommand(Container container)
        {
            _container = container;
        }

        public async Task Execute<TCommand>(TCommand command) where TCommand : IDefineCommand
        {
            using (_container.BeginExecutionContextScope())
            {
                var commands = _container.GetInstance<IExecuteCommands>();
                await commands.Execute(command);
            }
        }

        //    using (_container.BeginExecutionContextScope())
        //{

        //public async Task SaveChangesAsync()
        //    {
        //        var db = _container.GetInstance<IWriteEntities>();
        //        await db.SaveChangesAsync();
        //    }
        //}
    }

    public class ExecuteQuery
    {
        private readonly IExecuteQuandlRequests _queries;

        public ExecuteQuery(Container container)
        {
            _queries = GetQueryProcessor(container);
        }

        public IExecuteQuandlRequests GetQueryProcessor(Container container)
        {
            return container.GetInstance<IExecuteQuandlRequests>();
        }

        public TResult Execute<TResult>(IDefineQuandlRequest<TResult> quandlRequest)
        {
            return _queries.Execute(quandlRequest);
        }
    }
}