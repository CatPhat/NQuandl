using System.Collections.Generic;
using NQuandl.Npgsql.Api.DTO;

namespace NQuandl.Npgsql.Domain.Commands
{
    public class WriteCommand
    {
        public string TableName { get; set; }
        public IEnumerable<DbInsertData> Datas { get; set; }
    }
}