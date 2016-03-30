using System.Diagnostics;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Persistence.Transactions;
using NQuandl.Api.Transactions;
using SimpleInjector;

namespace NQuandl.Services.Transactions
{
    [UsedImplicitly]
    internal sealed class CommandExector : IExecuteCommands
    {
        private readonly Container _container;

        public CommandExector(Container container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public Task Execute(IDefineCommand command)
        {
            var handlerType = typeof (IHandleCommand<>).MakeGenericType(command.GetType());
            dynamic handler = _container.GetInstance(handlerType);
            return handler.Handle((dynamic) command);
        }
    }
}