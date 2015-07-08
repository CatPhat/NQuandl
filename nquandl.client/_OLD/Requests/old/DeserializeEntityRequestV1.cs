using NQuandl.Client.CompositionRoot;
using NQuandl.Client.Entities.Base;
using NQuandl.Client.Interfaces;
using NQuandl.Client.URIs;

namespace NQuandl.Client.Requests
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