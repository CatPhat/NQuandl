using System.Threading.Tasks;

namespace NQuandl.Api.Persistence.Transactions
{
    public interface IExecuteCommands
    {
        Task Execute(IDefineCommand command);
    }
}