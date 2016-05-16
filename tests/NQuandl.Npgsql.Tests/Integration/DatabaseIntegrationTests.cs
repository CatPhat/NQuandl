using NQuandl.Npgsql.Services.Database.Customization;
using NQuandl.Npgsql.Services.Database.Initialization;
using NQuandl.Npgsql.Tests.Integration._Fixtures;
using Xunit;

namespace NQuandl.Npgsql.Tests.Integration
{
    public class DatabaseIntegrationTests : DatabaseTests
    {
        public DatabaseIntegrationTests(DatabaseFixture fixture) : base(fixture) {}

        [Fact]
        public void DatabaseConnectionIsValid()
        {
            var connection = Connection.CreateConnection();
            connection.Open();
        }

        [Fact]
        public void DatabaseGreenfieldIntializationIsValid()
        {
            var initializer = new GreenfieldDbInitializer(new PostgresSqlScriptsCustomizer());
            initializer.Intialize(DbContext);
        }
    }
}