using System.Threading.Tasks;
using NQuandl.Api.Quandl;
using NQuandl.Api.Transactions;
using NQuandl.Domain.Persistence.Api.Entities;
using NQuandl.Domain.Persistence.Api.Transactions;
using NQuandl.Domain.Quandl.Responses;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace NQuandl.SimpleClient
{
    public static class QueryExtensions
    {
        private static readonly Container Container;

        static QueryExtensions()
        {
            Container = Bootstrapper.Bootstrap();
        }

        public static IQuandlClient GetQuandlClient()
        {
            return Container.GetInstance<IQuandlClient>();
        }

        public static async Task ExecuteCommand<TCommand>(this TCommand command) where TCommand : IDefineCommand
        {
            await new ExecuteCommand(Container).Execute(command);
        }

        public static async Task SaveChangesAsync()
        {
            await new ExecuteCommand(Container).SaveChangesAsync();
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


        public static IReadEntities GetReadEntities()
        {
            return Container.GetInstance<IReadEntities>();
        }

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

        public async Task SaveChangesAsync()
        {
            using (_container.BeginExecutionContextScope())
            {
                var db = _container.GetInstance<IWriteEntities>();
                await db.SaveChangesAsync();
            }
        }
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