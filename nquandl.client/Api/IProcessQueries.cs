namespace NQuandl.Client.Api
{
    public interface IProcessQueries
    {
        TResult Execute<TResult>(IDefineQuery<TResult> query);
    }
}