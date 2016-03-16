namespace NQuandl.Api.Transactions
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