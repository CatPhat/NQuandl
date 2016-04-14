using System.Threading.Tasks;

namespace NQuandl.Npgsql.Api.Transactions
{
    public interface IExecuteCommands
    {
        Task Execute(IDefineCommand command);
    }
}