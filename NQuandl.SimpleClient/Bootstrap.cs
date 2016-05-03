using System.Reflection;
using Microsoft.Extensions.Configuration;
using NQuandl.Client.Api.Transactions;
using NQuandl.Client.SimpleInjector.Extensions;
using NQuandl.Npgsql.Api.Transactions;

using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Packaging;

namespace NQuandl.SimpleClient
{
    public class Bootstrapper
    {
        private readonly IConfigurationRoot _configuration;
        public readonly Container Container;

        public Bootstrapper()
        {
            Container = new Container();

            var builder = new ConfigurationBuilder()
                .AddJsonFile(@"App_Data\config.json");


            _configuration = builder.Build();
            ComposeRoot(Container);
        }


        private void ComposeRoot(Container container)
        {
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
            Assembly[] requestHandlerAssemblies =
            {
                typeof (IHandleQuandlRequest<,>).Assembly,
                GetType().Assembly
            };
            Assembly[] commandHandlerAssemblies =
            {
                typeof (IHandleCommand<>).Assembly,
                GetType().Assembly
            };
            Assembly[] queryHandlerAssemblies =
            {
                typeof (IHandleQuery<,>).Assembly,
                GetType().Assembly
            };


            var packages = new IPackage[]
            {
                new Client.SimpleInjector.HttpClient.Package(_configuration.GetHttpClientConfiguration()),
                new Client.SimpleInjector.Logger.Package(),
                new Client.SimpleInjector.Quandl.Package(),
                new Client.SimpleInjector.RateGate.Package(),
                new Client.SimpleInjector.TaskQueue.Package(),
                new Client.SimpleInjector.Transactions.Package(requestHandlerAssemblies),
                new Npgsql.SimpleInjector.Package(),
                new Npgsql.SimpleInjector.Transactions.Commands.Package(),
                new Npgsql.SimpleInjector.Transactions.Queries.Package()
            };

            Container.RegisterPackages(packages);
            Container.Verify(VerificationOption.VerifyAndDiagnose);
        }
    }
}