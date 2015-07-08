using NQuandl.Client.Api;
using NQuandl.Client._OLD.Requests;
using NQuandl.Client._OLD.Requests.old;

namespace NQuandl.Queue
{
    public class QueueRequest<TEntity> where TEntity : QuandlEntity, new()
    {
        public QueryParametersV1 Options { get; set; }

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
