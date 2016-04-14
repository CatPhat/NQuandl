using System.Collections.Generic;

namespace NQuandl.Npgsql.Services.Helpers
{
    public class DbEntityAttributeMetadata
    {
        public string TableName { get; set; }
        public Dictionary<string, DbColumnInfoAttribute> PropertyNameAttributeDictionary { get; set; }
    }

   
}