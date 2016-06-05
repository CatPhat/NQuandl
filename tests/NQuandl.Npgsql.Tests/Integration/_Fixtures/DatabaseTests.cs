using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Metadata;
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
           
            CountryMetadataCache = fixture.CountryMetadataCache;
        }

        public IProvideDbConnection Connection { get; }
        public IDbContext DbContext { get; }
        public IEntityMetadataCache<Country> CountryMetadataCache { get; }
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