﻿using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Domain.Queries;
using NQuandl.Npgsql.Services.Mappers;
using NQuandl.Npgsql.Services.Metadata;
using Xunit;

namespace NQuandl.Npgsql.Tests
{
    public class EntitySqlMapperTest
    {
        //[Fact]
        //public void EntityBulkMapSqlStatementTest()
        //{
        //    var metadata = new EntityMetadata<MockDbEntity>();
        //    var sql = new EntitySqlMapper<MockDbEntity>(metadata);

        //    var statement = sql.GetBulkInsertData()
        //    Assert.Equal(statement, "COPY mock_db_entities (id,name,insert_date) FROM STDIN (FORMAT BINARY)");
        //}

        [Fact]
        public void EntitySelectMapSqlStatementTest()
        {
            var metadata = new EntityMetadataCache<Country>(new EntityMetadataCacheInitializer<Country>());
            var sqlEntityMapper = new EntitySqlMapper<Country>(new SqlMapper(), metadata);
            var sqlMapper = new SqlMapper();
            var objectMapper = new EntityObjectMapper<Country>(metadata);
            var query = new DataRecordsEnumerableByEntity<Country>(country => country.Iso31661Alpha3, "USA")
            {
                Limit = 10,
                OrderByColumn = x => x.Iso31661Alpha3
            };
          
            var statement = sqlEntityMapper.g(query);

            Assert.Equal(statement,
                "SELECT name,iso31661alpha3,iso31661numeric,iso31661alpha2,country_flag_url,altname,iso4217_currency_alphabetic_code,iso4217_country_name,iso4217_minor_units,iso4217_currency_name,iso4217_currency_numeric_code FROM countries WHERE iso31661alpha3 = 'USA' ORDER BY iso31661alpha3 LIMIT 10");
        }
    }
}