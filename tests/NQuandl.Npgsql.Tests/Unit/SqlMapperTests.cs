using System;
using NQuandl.Npgsql.Domain.Queries;
using NQuandl.Npgsql.Services.Extensions;
using NQuandl.Npgsql.Services.Mappers;
using NQuandl.Npgsql.Tests.Unit.Mocks;
using Xunit;

namespace NQuandl.Npgsql.Tests.Unit
{
    public class SqlMapperTests
    {
        [Fact]
        public void SqlInsertStatementTest()
        {
            const int id = 999;
            var insertDate = DateTime.Now;
            const string name = "mockedName";

            var sqlMapper = new SqlMapper();
            var metadata = MockMetadataFactory<MockDbEntity>.Metadata;

            var entity = new MockDbEntity
            {
                Id = id,
                InsertDate = insertDate,
                Name = name
            };

            var insertDatas = metadata.CreateInsertDatas(entity);
            var insertSqlStatement = sqlMapper.GetInsertSql(metadata.GetTableName(), insertDatas);

            Assert.Equal("INSERT INTO mock_db_entities (id,name,insert_date) VALUES (:id,:name,:insert_date);", insertSqlStatement);
        }

        [Fact]
        public void SqlBulkInsertStatementTest()
        {
            const int id = 999;
            var insertDate = DateTime.Now;
            const string name = "mockedName";

            var sqlMapper = new SqlMapper();
            var metadata = MockMetadataFactory<MockDbEntity>.Metadata;

            var entity = new MockDbEntity
            {
                Id = id,
                InsertDate = insertDate,
                Name = name
            };

            var insertDatas = metadata.CreateInsertDatas(entity);
            var insertSqlStatement = sqlMapper.GetBulkInsertSql(metadata.GetTableName(), insertDatas);

            Assert.Equal("COPY mock_db_entities (id,name,insert_date) FROM STDIN (FORMAT BINARY)", insertSqlStatement);
        }

        [Fact]
        public void SqlSelectByStatementTest()
        {
            const string queryString = "valueToBeQueried";
            const int limit = 10;
            const int offset = 0;


            var sqlMapper = new SqlMapper();
            var metadata = MockMetadataFactory<MockDbEntity>.Metadata;
            
            var query = metadata.CreateDataRecordsQuery(new DataRecordsEnumerableByEntity<MockDbEntity>(x => x.Name, queryString)
            {
                Limit = limit,
                Offset = offset,
                OrderByColumn = x => x.Id
            });
            var insertSqlStatement = sqlMapper.GetSelectSqlBy(query);

            Assert.Equal($"SELECT id,name,insert_date FROM mock_db_entities WHERE name = '{queryString}' ORDER BY id LIMIT {limit} OFFSET {offset}", insertSqlStatement);
        }

        
    }
}
