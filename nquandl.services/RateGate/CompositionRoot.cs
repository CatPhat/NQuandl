using System;
using NQuandl.Api.Quandl;
using SimpleInjector;

namespace NQuandl.Services.RateGate
{
    public static class CompositionRoot
    {
        public static void RegisterRateGate(this Container container)
        {
            container.Register<IRateGate>(() => new RateGate(1, TimeSpan.FromMilliseconds(1000)), Lifestyle.Singleton);
        }
    }
}