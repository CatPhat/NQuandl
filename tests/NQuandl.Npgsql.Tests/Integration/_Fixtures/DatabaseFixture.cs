using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Database;
using NQuandl.Npgsql.Services.Database.Configuration;
using NQuandl.Npgsql.Services.Mappers;

namespace NQuandl.Npgsql.Tests.Integration._Fixtures
{
    public class DatabaseFixture
    {
        public DatabaseFixture()
        {

            var configuration = new DebugConnectionConfiguration();
            var connection = new DbConnectionProvider(configuration);
            var sqlMapper = new SqlMapper();
            Connection = connection;
            DbContext = new DbContex(connection, sqlMapper);
        }
        public IProvideDbConnection Connection { get; }
        public IDbContext DbContext { get; }

        public Country TestCountry => new Country
        {
            AltName = "testAltName",
            CountryFlagUrl = @"http://testFlagUrl",
            Iso31661Alpha3 = "AAA",
            Iso31661Alpha2 = "BB",
            Iso31661Numeric = 333,
            Iso4217CountryName = "testCountryName",
            Iso4217CurrencyAlphabeticCode = "testCurrencyCode",
            Iso4217CurrencyName = "testCurrencyName",
            Iso4217CurrencyNumericCode = 444,
            Iso4217MinorUnits = 555,
            Name = "testName"
        };
    }
}