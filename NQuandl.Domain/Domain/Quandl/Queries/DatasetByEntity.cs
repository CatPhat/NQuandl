using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Quandl;
using NQuandl.Api.Quandl.Helpers;
using NQuandl.Api.Transactions;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Domain.Quandl.Queries
{
    // https://www.quandl.com/api/v3/datasets/WIKI/FB.json
    public class DatasetByEntity<TEntity> : IDefineQuery<Task<DatabaseDataset<TEntity>>>
        where TEntity : QuandlEntity
    {
        public ResponseFormat ResponseFormat => ResponseFormat.JSON;

        public int? Limit { get; set; }
        public int? Rows { get; set; }
        public int? ColumnIndex { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Order? Order { get; set; }
        public Collapse? Collapse { get; set; }
        public Transform? Transform { get; set; }
        public string ApiVersion => RequestParameterConstants.ApiVersion;
    }

    public class HandleDatasetByEntity<TEntity> : IHandleQuery<DatasetByEntity<TEntity>, Task<DatabaseDataset<TEntity>>>
        where TEntity : QuandlEntity
    {
        private readonly IQuandlClient _client;
        private readonly IMapObjectToEntity<TEntity> _mapper;


        public HandleDatasetByEntity([NotNull] IQuandlClient client, IMapObjectToEntity<TEntity> mapper)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            if (mapper == null) throw new ArgumentNullException(nameof(mapper));


            _client = client;
            _mapper = mapper;
        }

        public async Task<DatabaseDataset<TEntity>> Handle(DatasetByEntity<TEntity> query)
        {
            var result = await _client.GetAsync<DatabaseDataset<TEntity>>(query.ToQuandlClientRequestParameters());
            if (result.QuandlClientResponseInfo.IsStatusSuccessCode)
            {
                result.Entities = result.dataset.data.Select(_mapper.MapEntity);
            }
            
            return result;
        }
    }
}