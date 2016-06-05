using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NQuandl.Npgsql.Domain.Commands;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Domain.Queries;
using NQuandl.Npgsql.Services.Database.Customization;
using NQuandl.Npgsql.Services.Database.Initialization;
using NQuandl.Npgsql.Tests.Integration._Fixtures;
using Xunit;

namespace NQuandl.Npgsql.Tests.Integration
{
    public class DatabaseIntegrationTests : DatabaseTests
    {
        public DatabaseIntegrationTests(DatabaseFixture fixture) : base(fixture) {}

        private IObservable<Country> GetCountries()
        {
            var query = new EntitiesObservableBy<Country>();
            var queryHandler = new HandleEntitiesObservableBy<Country>(CountryMetadataCache, DbContext);
            var result = queryHandler.Handle(query);
            return result;
        }

        private async Task InsertCountry()
        {
            var insertCommand = new WriteEntity<Country>(TestCountry);
            var insertCommandHandler = new HandleWriteEntity<Country>(DbContext, CountryMetadataCache);
            await insertCommandHandler.Handle(insertCommand);
        }

        private async Task DeleteCountries()
        {
            var deleteCommand = new DeleteEntities<Country>(x => x.Name, TestCountry.Name);
            var deleteCommandHandler = new HandleDeleteEntities<Country>(DbContext, CountryMetadataCache);
            await deleteCommandHandler.Handle(deleteCommand);
        }

        [Fact]
        public async void BulkInsertCountries()
        {
            var existingCountries = await GetCountries().ToList();
            if (existingCountries.Any())
            {
                await DeleteCountries();
            }

            var countries = new List<Country>();
            for (var i = 0; i < 100; i++)
            {
                var country = TestCountry;
                countries.Add(country);
            }

            var command = new BulkWriteEntities<Country>(countries);
            var commandHandler = new HandleBulkWriteEntities<Country>(CountryMetadataCache, DbContext);
            await commandHandler.Handle(command);

            var results = GetCountries().ToEnumerable();

            countries.ShouldBeEquivalentTo(results);
        }

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

        [Fact]
        public async void DeleteCountryCommand()
        {
            await InsertCountry();
            var results1 = await GetCountries().ToList();
            Assert.NotEmpty(results1);

            await DeleteCountries();

            var results2 = await GetCountries().ToList();
            Assert.Empty(results2);
        }

        [Fact]
        public async void EntitiesObservableByQuery()
        {
            await InsertCountry();
            var results = GetCountries();
            var firstResult = await results.FirstOrDefaultAsync();
            Assert.NotNull(firstResult);
            firstResult.ShouldBeEquivalentTo(TestCountry);
        }

        [Fact]
        public async void InsertCountryDataCommand()
        {
            await InsertCountry();
        }
    }
}