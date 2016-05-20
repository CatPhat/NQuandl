using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Domain.Entities;
using Xunit;

namespace NQuandl.Npgsql.Tests.Integration._Fixtures
{
    public abstract class DatabaseTests : IClassFixture<DatabaseFixture>
    {
        protected DatabaseTests(DatabaseFixture fixture)
        {
            DbContext = fixture.DbContext;
            Connection = fixture.Connection;
            TestCountry = fixture.TestCountry;
        }

        public IProvideDbConnection Connection { get; }
        public IDbContext DbContext { get; }
        public Country TestCountry { get; }
    }
}