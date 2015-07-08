using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Domain.Responses;
using NQuandl.Client.Helpers;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;
using NQuandl.Client.Responses;

namespace NQuandl.Client.Domain.Entities
{
    public class GetJsonFredGdp : IDefineQuery<Task<DeserializedJsonResponseV1<FredGdp>>>
    {
        public QueryParametersV1 QueryParametersV1 { get; set; }
    }

    public class HandleGetJsonFredGdp : IHandleQuery<GetJsonFredGdp, Task<DeserializedJsonResponseV1<FredGdp>>>
    {
        private readonly IQuandlClient _client;

        public HandleGetJsonFredGdp(IQuandlClient client)
        {
            if (client == null) throw new ArgumentNullException("client");
            _client = client;
        }

        public async Task<DeserializedJsonResponseV1<FredGdp>> Handle(GetJsonFredGdp query)
        {
            var requiredParameters = new PathSegmentParametersV1
            {
                ApiVersion = RequestParameterConstants.ApiVersion1,
                QuandlCode = FredGdp.Constants.QuandlCode,
                ResponseFormat = ResponseFormat.JSON.ToString()
            };

            var requestParameters = new QuandlRequestParameters
            {
                PathSegment = requiredParameters.ToPathSegment(),
                QueryParameters = query.QueryParametersV1.ToQueryParameterDictionary()
            };

            var response = await _client.GetAsync(requestParameters);
            var deserializedJson = await Deserialize(response);
            var deserializedResponse = new DeserializedJsonResponseV1<FredGdp>
            {
                DeserializedJson = deserializedJson,
                Entities = MapToEntities(deserializedJson.Data)
            };

            return deserializedResponse;
        }

        private async Task<JsonResponseV1<FredGdp>> Deserialize(string rawResponse)
        {
            return await rawResponse.DeserializeToObjectAsync<JsonResponseV1>();
        }

        private IEnumerable<FredGdp> MapToEntities(object[][] objects)
        {
            return objects.Select(MapToEntity);
        }

        private FredGdp MapToEntity(object[] objects)
        {
            return new FredGdp
            {
                Date = objects[0].ToString(),
                Value = double.Parse(objects[1].ToString())
            };
        }
    }
}