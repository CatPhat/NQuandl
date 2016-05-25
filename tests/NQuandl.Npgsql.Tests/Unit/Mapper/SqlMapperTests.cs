using System;
using NQuandl.Npgsql.Domain.Commands;
using NQuandl.Npgsql.Domain.Queries;
using NQuandl.Npgsql.Services.Extensions;
using NQuandl.Npgsql.Services.Mappers;
using NQuandl.Npgsql.Tests.Unit.Mocks;
using NQuandl.Npgsql.Tests.Unit._Fixtures;
using Xunit;

namespace NQuandl.Npgsql.Tests.Unit.Mapper
{
    public class SqlMapperTests : MockMetadataTests
    {
        public SqlMapperTests(MockMetadataFixture mockMetadata) : base(mockMetadata) {}

        [Fact]
        public void SqlInsertStatementTest()
        {
            const int id = 999;
            var insertDate = DateTime.Now;
            const string name = "mockedName";

            var sqlMapper = new SqlMapper();
       

            var entity = new MockDbEntity
            {
                Id = id,
                InsertDate = insertDate,
                Name = name
            };

            var insertDatas = MockMetadata.CreateInsertDatas(entity);
            var insertSqlStatement = sqlMapper.GetInsertSql(MockMetadata.GetTableName(), insertDatas);

            Assert.Equal("INSERT INTO mock_db_entities (id,name,insert_date) VALUES (:id,:name,:insert_date);", insertSqlStatement);
        }

        [Fact]
        public void SqlBulkInsertStatementTest()
        {
            const int id = 999;
            var insertDate = DateTime.Now;
            const string name = "mockedName";

            var sqlMapper = new SqlMapper();
          

            var entity = new MockDbEntity
            {
                Id = id,
                InsertDate = insertDate,
                Name = name
            };

            var insertDatas = MockMetadata.CreateInsertDatas(entity);
            var insertSqlStatement = sqlMapper.GetBulkInsertSql(MockMetadata.GetTableName(), insertDatas);

            Assert.Equal("COPY mock_db_entities (id,name,insert_date) FROM STDIN (FORMAT BINARY)", insertSqlStatement);
        }

        [Fact]
        public void SqlSelectByStatementTest()
        {
            const string queryString = "valueToBeQueried";
            const int limit = 10;
            const int offset = 0;


            var sqlMapper = new SqlMapper();
         
            
            var query = MockMetadata.CreateDataRecordsQuery(new DataRecordsEnumerableByEntity<MockDbEntity>(x => x.Name, queryString)
            {
                Limit = limit,
                Offset = offset,
                OrderByColumn = x => x.Id
            });
            var insertSqlStatement = sqlMapper.GetSelectSqlBy(query);

            Assert.Equal($"SELECT id,name,insert_date FROM mock_db_entities WHERE name = '{queryString}' ORDER BY id LIMIT {limit} OFFSET {offset}", insertSqlStatement);
        }

        [Fact]
        public void SqlDeleteByStringStatementTest()
        {
            const string tableName = "test_table";
            const string whereColumn = "test_column";
            const string deleteValue = "test_delete_value";
            var command = new DeleteCommand(tableName, whereColumn, deleteValue);
            var sqlMapper = new SqlMapper();
            var statement = sqlMapper.GetDeleteRowSql(command);

            Assert.Equal("DELETE FROM test_table WHERE test_column = 'test_delete_value'", statement);
        }

        [Fact]
        public void SqlDeleteByIntegerStatementTest()
        {
            const string tableName = "test_table";
            const string whereColumn = "test_column";
            const int deleteValue = 777;
            var command = new DeleteCommand(tableName, whereColumn, deleteValue);
            var sqlMapper = new SqlMapper();
            var statement = sqlMapper.GetDeleteRowSql(command);

            Assert.Equal("DELETE FROM test_table WHERE test_column = 777", statement);
        }


    }
}
