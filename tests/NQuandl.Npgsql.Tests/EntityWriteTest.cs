using System;
using System.Data.SqlClient;
using NQuandl.Npgsql.Services.Mappers;
using NQuandl.Npgsql.Services.Metadata;
using NQuandl.Npgsql.Tests.Mocks;
using Xunit;

namespace NQuandl.Npgsql.Tests
{
    public class EntityWriteTest
    {
        [Fact]
        public void EntityInsertDataTest()
        {
            const int id = 666;
            const string name = "mockedEntity";
            var dateTime = DateTime.Now;

            var entity = new MockDbEntity
            {
                Id = id,
                InsertDate = dateTime,
                Name = name
            };
            var sqlMapper = new EntitySqlMapper<MockDbEntity>(new EntityMetadata<MockDbEntity>());

            var insertData = sqlMapper.GetInsertData(entity);

            Assert.Equal(insertData.Parameters.Length, 3);
            Assert.Equal(insertData.Parameters[0].Value, id);
            Assert.Equal(insertData.Parameters[1].Value, name);
            Assert.Equal(insertData.Parameters[2].Value, dateTime);

            Assert.Equal(insertData.SqlStatement, "INSERT INTO mock_db_entities (id,name,insert_date) VALUES (:id,:name,:insert_date);");
        }

        [Fact]
        public void EntityInsertWithSerialIdTest()
        {
            const int id = 666;
            const string name = "mockedEntity";
            var dateTime = DateTime.Now;

            var entity = new MockDbEntityWithSerialId
            {
                Id = id,
                InsertDate = dateTime,
                Name = name
              
            };
            var sqlMapper = new EntitySqlMapper<MockDbEntityWithSerialId>(new EntityMetadata<MockDbEntityWithSerialId>());

            var insertData = sqlMapper.GetInsertData(entity);

            Assert.Equal(insertData.Parameters.Length, 2);
            Assert.Equal(insertData.Parameters[0].Value, name);
            Assert.Equal(insertData.Parameters[1].Value, dateTime);
            Assert.Equal(insertData.SqlStatement, "INSERT INTO mock_db_entities_with_serial_id (name,insert_date) VALUES (:name,:insert_date);");
        }
    }


}