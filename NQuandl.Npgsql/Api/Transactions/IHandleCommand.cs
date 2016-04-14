using System.Threading.Tasks;

namespace NQuandl.Npgsql.Api.Transactions
{
    public interface IHandleCommand<in TCommand> where TCommand : IDefineCommand
    {
        Task Handle(TCommand command);
    }
}