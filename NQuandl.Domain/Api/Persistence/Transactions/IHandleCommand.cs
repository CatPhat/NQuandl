using System.Threading.Tasks;

namespace NQuandl.Api.Persistence.Transactions
{
    public interface IHandleCommand<in TCommand> where TCommand : IDefineCommand
    {
        Task Handle(TCommand command);
    }
}