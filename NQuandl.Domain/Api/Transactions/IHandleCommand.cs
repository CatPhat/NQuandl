using System.Threading.Tasks;

namespace NQuandl.Api.Transactions
{
    public interface IHandleCommand<in TCommand> where TCommand : IDefineCommand
    {
        Task Handle(TCommand command);
    }
}