namespace NQuandl.PostgresEF7.Api.Transactions
{
    public abstract class BaseEntityCommand
    {
        protected BaseEntityCommand()
        {
            Commit = true;
        }

        internal bool Commit { get; set; }
    }
}