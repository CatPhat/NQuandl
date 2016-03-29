using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NQuandl.Domain.Quandl.Responses
{
    
    public class JsonResultDatasetSearch
    {
        [JsonProperty("datasets")]
        public JsonDatasetSearchDataset[] JsonSearchDatasets { get; set; }

        [JsonProperty("meta")]
        public JsonDatasetSearchMetadata Metadata { get; set; }
    }
}
