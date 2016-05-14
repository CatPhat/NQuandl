using System.Threading.Tasks;

namespace NQuandl.PostgresEF7.Api.Transactions
{
    public interface IHandleCommand<in TCommand> where TCommand : IDefineCommand
    {
        Task Handle(TCommand command);
    }
}