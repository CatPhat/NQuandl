using System.Threading.Tasks;

namespace NQuandl.PostgresEF7.Api.Transactions
{
    public interface IExecuteCommands
    {
        Task Execute(IDefineCommand command);
    }
}