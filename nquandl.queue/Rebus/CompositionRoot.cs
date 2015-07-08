using System;
using System.Reflection;
using NQuandl.Client.Requests;
using Rebus;
using Rebus.Configuration;
using Rebus.Logging;
using Rebus.Persistence.SqlServer;
using Rebus.SimpleInjector;
using Rebus.Transports.Sql;
using SimpleInjector;

namespace NQuandl.Queue.Rebus
{
    public static class CompositionRoot
    {
        public static void RegisterRebus(this Container container, params Assembly[] assemblies)
        {
            var adapter = new SimpleInjectorAdapter(container);
            var bus = Configure.With(adapter)
                .Logging(l => l.ColoredConsole(LogLevel.Error))
                .Transport(
                    t =>
                        t.UseSqlServer(@"server=SHIVA9.;initial catalog=RebusInputQueue;integrated security=sspi",
                            "thequeue", "my-app.input", "my-app.error").EnsureTableIsCreated())
                .Timeouts(
                    x =>
                        x.Use(
                            new SqlServerTimeoutStorage(
                                @"server=SHIVA9.;initial catalog=RebusInputQueue;integrated security=sspi",
                                "timeouts").EnsureTableIsCreated()))
                .MessageOwnership(x => x.Use(new DetermineQueueOwnership()))
                .CreateBus()
                .Start();
        }
    }

    public class DetermineQueueOwnership : IDetermineMessageOwnership
    {
        public string GetEndpointFor(Type messageType)
        {
            if (messageType == typeof(DeserializeMetadataRequestV2))
            {
                return "my-app.input";
            }
            throw new Exception("no endpoint for message type");
        }
    }
}