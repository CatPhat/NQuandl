using System.Collections.Generic;

namespace NQuandl.Npgsql.Api.DTO
{
    public class InsertDataCommand
    {
        public string TableName { get; set; }
        public IEnumerable<DbImportData> DbDatas { get; set; }
    }
}