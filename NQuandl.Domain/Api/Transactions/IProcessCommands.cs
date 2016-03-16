using System.Threading.Tasks;

namespace NQuandl.Api.Transactions
{
    public interface IProcessCommands
    {
        Task Execute(IDefineCommand command);
    }
}