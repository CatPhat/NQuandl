﻿using System.Reflection;
using NQuandl.Api.Transactions;
using NQuandl.Domain.Persistence.Api.Transactions;
using SimpleInjector;

namespace NQuandl.Services.Quandl.Transactions
{
    public static class CompositionRoot
    {
        public static void RegisterQuandlRequestTransactions(this Container container, params Assembly[] assemblies)
        {
            assemblies = assemblies ?? new[] {Assembly.GetAssembly(typeof (IHandleQuandlRequest<,>))};

            container.Register<IExecuteQuandlRequests, RequestExecutor>(Lifestyle.Singleton);
            container.Register(typeof (IHandleQuandlRequest<,>), assemblies);
        }

        public static void RegisterCommandTransactions(this Container container, params Assembly[] assemblies)
        {
            assemblies = assemblies ?? new[] {Assembly.GetAssembly(typeof (IHandleCommand<>))};

            container.Register<IExecuteCommands, CommandExecutor>(Lifestyle.Singleton);
            container.Register(typeof (IHandleCommand<>), assemblies);
        }

        public static void RegisterQueryTransactions(this Container container, params Assembly[] assemblies)
        {
            assemblies = assemblies ?? new[] {Assembly.GetAssembly(typeof (IHandleQuery<,>))};

            container.RegisterSingleton<IExecuteQueries, QueryExecutor>();
            container.Register(typeof (IHandleQuery<,>), assemblies);
        }
    }
}