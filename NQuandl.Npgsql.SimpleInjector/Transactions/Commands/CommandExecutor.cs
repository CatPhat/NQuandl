﻿using System.Diagnostics;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api.Transactions;
using SimpleInjector;

namespace NQuandl.Npgsql.SimpleInjector.Transactions.Commands
{
    [UsedImplicitly]
    internal sealed class CommandExecutor : IExecuteCommands
    {
        private readonly Container _container;

        public CommandExecutor(Container container)
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