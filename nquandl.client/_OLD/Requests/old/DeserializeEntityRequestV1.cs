using NQuandl.Client.Api;
using NQuandl.Client.CompositionRoot;
using NQuandl.Client._OLD.Interfaces.old;
using NQuandl.Client._OLD.URIs;

namespace NQuandl.Client._OLD.Requests.old
{
    public class DeserializeEntityRequestV1<TEntity> 
        where TEntity : QuandlEntity, new()
    {
        private readonly IMapJsonToEntity<TEntity> _mapper;
        private readonly string _quandlCode;

        public DeserializeEntityRequestV1()
        {
            _mapper = Bootstapper.GetMapper<TEntity>();
            _quandlCode = new TEntity().QuandlCode;
        }

        public QueryParametersV1 Options { get; set; }

        public IQuandlUri Uri
        {
            get { return new QuandlJsonUriV1(_quandlCode, Options); }
        }

        public IMapJsonToEntity<TEntity> Mapper
        {
            get { return _mapper; }
        }
    }
}