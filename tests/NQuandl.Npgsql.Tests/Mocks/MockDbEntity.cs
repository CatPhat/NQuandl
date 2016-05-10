using System;
using NpgsqlTypes;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Services.Attributes;

namespace NQuandl.Npgsql.Tests.Mocks
{
    [DbTableName("mock_db_entities")]
    public class MockDbEntity : DbEntity
    {
        [DbColumnInfo(0, "id", NpgsqlDbType.Integer)]
        public int Id { get; set; }

        [DbColumnInfo(1, "name", NpgsqlDbType.Text, true)]
        public string Name { get; set; }

        [DbColumnInfo(2, "insert_date", NpgsqlDbType.Timestamp)]
        public DateTime InsertDate { get; set; }
    }

    [DbTableName("mock_db_entities_with_serial_id")]
    public class MockDbEntityWithSerialId : DbEntity
    {
        [DbColumnInfo(0, "id", NpgsqlDbType.Integer, isStoreGenerated: true)]
        public int Id { get; set; }

        [DbColumnInfo(1, "name", NpgsqlDbType.Text, true)]
        public string Name { get; set; }

        [DbColumnInfo(2, "insert_date", NpgsqlDbType.Timestamp)]
        public DateTime InsertDate { get; set; }
    }
}