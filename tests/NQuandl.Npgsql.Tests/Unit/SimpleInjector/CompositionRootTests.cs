using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Npgsql.SimpleInjector.CompositionRoot;
using NQuandl.Npgsql.Tests.Unit.SimpleInjector._Fixtures;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using Xunit;

namespace NQuandl.Npgsql.Tests.Unit.SimpleInjector
{
    public class CompositionRootTests : SimpleInjectorContainerTests
    {
        public CompositionRootTests(CompositionRootFixture fixture) : base(fixture) {}

        [Fact]
        public void ComposeRoot_ComposesVerifiedRoot_WithoutDiagnosticsWarnings()
        {
            Container.Verify();
            var results = Analyzer.Analyze(Container);
            Assert.Equal(false, results.Any());
        }

        [Fact]
        public void ComposeRoot_AllowsOverridingRegistrations()
        {
            Assert.Equal(false, Container.Options.AllowOverridingRegistrations);
        }

        [Fact]
        public void ComposeRoot_UsesDefaultSettings_WhenNoneArePassed()
        {
            var container = new Container();
            container.ComposeRoot(null);
        }

        [Fact]
        public void ComposeRoot_RegistersIServiceProvider_UsingOwnContainer_AsSingleton()
        {
            var instance = Container.GetInstance<IServiceProvider>();
            var registration = Container.GetRegistration(typeof(IServiceProvider));
            Assert.NotNull(instance);
            Assert.Equal(Container, instance);
            Assert.Equal(Lifestyle.Singleton, registration.Lifestyle);
        }

       
    }
}
