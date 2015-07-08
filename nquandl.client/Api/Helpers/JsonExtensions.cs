using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NQuandl.Client.Helpers
{
    public static class JsonExtensions
    {
        public static async Task<T> DeserializeToObjectAsync<T>(this string jsonResponse) where T : class
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(jsonResponse));
        }
    }
}