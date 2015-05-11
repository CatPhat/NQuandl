﻿using NQuandl.Client.CompositionRoot;
using NQuandl.Client.Entities.Base;
using NQuandl.Client.Interfaces;
using NQuandl.Client.URIs;

namespace NQuandl.Client.Requests
{
    public class DeserializeEntityRequest<TEntity> : IDeserializedEntityRequest<TEntity>
        where TEntity : QuandlEntity, new()
    {
        private readonly IMapData<TEntity> _mapper;
        private readonly string _quandlCode;

        public DeserializeEntityRequest()
        {
            _mapper = Bootstapper.GetMapper<TEntity>();
            _quandlCode = new TEntity().QuandlCode;
        }

        public RequestParameterOptions Options { get; set; }

        public IContainUri Uri
        {
            get { return new QuandlJsonUriV1(_quandlCode, Options); }
        }

        public IMapData<TEntity> Mapper
        {
            get { return _mapper; }
        }
    }
}