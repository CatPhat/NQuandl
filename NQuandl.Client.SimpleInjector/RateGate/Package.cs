using System;
using NQuandl.Client.Api.Quandl;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Client.SimpleInjector.RateGate
{
    public class Package : IPackage
    {
        //todo set as configuration
        public void RegisterServices(Container container)
        {
            container.Register<IRateGate>(() => new Services.RateGate.RateGate(2000, TimeSpan.FromMinutes(10)), Lifestyle.Singleton);
        }
    }
}