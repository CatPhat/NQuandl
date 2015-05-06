using Newtonsoft.Json;

namespace NQuandl.Client.Responses
{
    public class DeserializedJsonResponse<TResponse> where TResponse : JsonResponse
    {
        public readonly TResponse JsonResponse;

        public DeserializedJsonResponse(string response)
        {
            JsonResponse = JsonConvert.DeserializeObject<TResponse>(response);
        }
    }
}