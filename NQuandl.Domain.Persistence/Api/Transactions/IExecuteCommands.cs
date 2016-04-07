using System.Threading.Tasks;

namespace NQuandl.Domain.Persistence.Api.Transactions
{
    public interface IExecuteCommands
    {
        Task Execute(IDefineCommand command);
    }
}