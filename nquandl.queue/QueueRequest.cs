using NQuandl.Client.Entities.Base;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;

namespace NQuandl.Queue
{
    public class QueueRequest<TEntity> where TEntity : QuandlEntity, new()
    {
        public RequestParameterOptions Options { get; set; }

        public DeserializeEntityRequest<TEntity> DeserializeEntityRequest
        {
            get
            {
                return new DeserializeEntityRequest<TEntity>
                {
                    Options = Options
                };
            }
        }
    }
}
