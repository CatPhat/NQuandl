using NQuandl.Client.Entities.Base;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;

namespace NQuandl.Queue
{
    public class QueueRequest<TEntity> where TEntity : QuandlEntity, new()
    {
        public RequestOptionsV1 Options { get; set; }

        public DeserializeEntityRequestV1<TEntity> DeserializeEntityRequestV1
        {
            get
            {
                return new DeserializeEntityRequestV1<TEntity>
                {
                    Options = Options
                };
            }
        }
    }
}
