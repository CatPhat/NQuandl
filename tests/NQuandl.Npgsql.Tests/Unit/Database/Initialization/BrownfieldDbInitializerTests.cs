using NQuandl.Npgsql.Services.Database;
using NQuandl.Npgsql.Services.Database.Configuration;
using NQuandl.Npgsql.Services.Database.Initialization;
using NQuandl.Npgsql.Services.Mappers;
using Xunit;

namespace NQuandl.Npgsql.Tests.Unit.Database.Initialization
{
    public class BrownfieldDbInitializerTests
    {
        [Fact]
        public void InitializeDatabase_HasNoSideEffects()
        {
            var dbInitializer = new BrownfieldDbInitializer();
            var connection = new DbConnectionProvider(new DebugConnectionConfiguration());
            var db = new DbContex(connection, new SqlMapper());
            dbInitializer.Intialize(db);
            Assert.NotNull(db);
        }
    }
}