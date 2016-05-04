using System;
using System.Data;
using Moq;
using NQuandl.Npgsql.Services.Mappers;
using NQuandl.Npgsql.Services.Metadata;
using NQuandl.Npgsql.Tests.Mocks;
using Xunit;

namespace NQuandl.Npgsql.Tests
{
    public class EntityColumnTypeMapperTest
    {
        [Fact]
        public void TestMap()
        {
            const int idValue = 666;
            const string nameValue = "mockedName";
            var insertDateValue = DateTime.Now;

            var datarecord = new Mock<IDataRecord>();
            datarecord.Setup(x => x[0]).Returns(idValue);
            datarecord.Setup(x => x[1]).Returns(nameValue);
            datarecord.Setup(x => x[2]).Returns(insertDateValue);
            datarecord.Setup(x => x.IsDBNull(0)).Returns(false);
            datarecord.Setup(x => x.IsDBNull(1)).Returns(false);
            datarecord.Setup(x => x.IsDBNull(2)).Returns(false);

            var metadata = new EntityMetadata<MockDbEntity>();
            var entity = metadata.CreatEntity(datarecord.Object);

            Assert.Equal(idValue, entity.Id);
            Assert.Equal(nameValue, entity.Name);
            Assert.Equal(insertDateValue, entity.InsertDate);
        }
    }
}