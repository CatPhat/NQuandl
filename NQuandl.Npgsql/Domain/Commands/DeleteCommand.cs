namespace NQuandl.Npgsql.Domain.Commands
{
    public class DeleteCommand
    {
        public DeleteCommand(string tableName, string whereColumn, string deleteByValue)
        {
            TableName = tableName;
            WhereColumn = whereColumn;
            DeleteByString = deleteByValue;
        }

        public DeleteCommand(string tableName, string whereColumn, int deleteByValue)
        {
            TableName = tableName;
            WhereColumn = whereColumn;
            DeleteByInteger = deleteByValue;
        }

        public string TableName { get; }
        public string WhereColumn { get; }
        public string DeleteByString { get; }
        public int? DeleteByInteger { get; }
    }
}