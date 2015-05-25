using System.Threading;
using System.Threading.Tasks;

namespace NQuandl.Queue.Bus
{
    public interface IHandleAsync<in TMessage>
    {
        Task HandleAsync(TMessage message, CancellationToken cancellationToken);
    }
}