namespace NQuandl.Api
{
    public interface IProcessQueries
    {
        TResult Execute<TResult>(IDefineQuery<TResult> query);
    }
}