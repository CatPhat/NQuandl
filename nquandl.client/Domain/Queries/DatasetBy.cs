using System;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Api.Helpers;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.Queries
{
    // https://www.quandl.com/api/v3/datasets/WIKI/FB.json
    public class DatasetBy<TEntity> : IDefineQuery<Task<DatabaseDataset<TEntity>>>
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

    public class HandleDatasetBy<TEntity> : IHandleQuery<DatasetBy<TEntity>, Task<DatabaseDataset<TEntity>>>
        where TEntity : QuandlEntity
    {
        private readonly IProcessQueries _queries;
        private readonly IMapObjectToEntity<TEntity> _mapper;

        public HandleDatasetBy(IProcessQueries queries, IMapObjectToEntity<TEntity> mapper)
        {
            if (queries == null) throw new ArgumentNullException(nameof(queries));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            _queries = queries;
            _mapper = mapper;
        }

        public async Task<DatabaseDataset<TEntity>> Handle(DatasetBy<TEntity> query)
        {
            var entity = (TEntity) Activator.CreateInstance(typeof (TEntity));

            var quandlClientRequestParameters = new QuandlClientRequestParameters
            {
                PathSegment =
                    $"{query.ApiVersion}/datasets/{entity.DatabaseCode}/{entity.DatasetCode}.{query.ResponseFormat.GetStringValue()}",
                QueryParameters = query.ToRequestParameterDictionary()
            };

            var result =
                await _queries.Execute(new QuandlQueryBy<DatabaseDataset<TEntity>>(quandlClientRequestParameters));
            result.Entities = result.dataset.data.Select(_mapper.MapEntity);
            return result;
        }
    }
}