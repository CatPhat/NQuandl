using SimpleInjector;
using Xunit;

namespace NQuandl.Npgsql.Tests.Unit.SimpleInjector._Fixtures
{
    public abstract class SimpleInjectorContainerTests : IClassFixture<CompositionRootFixture>
    {
        protected Container Container { get; private set; }

        protected SimpleInjectorContainerTests(CompositionRootFixture fixture)
        {
            Container = fixture.Container;
        }

       
    }
}