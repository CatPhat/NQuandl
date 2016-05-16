using NQuandl.Npgsql.Api;
using Xunit;

namespace NQuandl.Npgsql.Tests.Integration._Fixtures
{
    public abstract class DatabaseTests : IClassFixture<DatabaseFixture>
    {
        protected DatabaseTests(DatabaseFixture fixture)
        {
            DbContext = fixture.DbContext;
            Connection = fixture.Connection;
        }

        public IProvideDbConnection Connection { get; }
        public IDbContext DbContext { get; }
    }
}