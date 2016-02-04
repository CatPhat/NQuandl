namespace NQuandl.Client.Api
{
    public interface IHandleQuandlQuery<in TQuandlQuery, out TQuandlResult>
        where TQuandlQuery : IDefineQuandlQuery<TQuandlResult>
    {
        TQuandlResult Handle(TQuandlQuery query);
    }
}