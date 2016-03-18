using System.IO;
using System.Threading.Tasks;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Api.Quandl.Helpers
{
    public static class ResponseExtensions
    {
        //public static async Task<RawHttpContent> ToRawHttpContent(this QuandlClientResponse response)
        //{
        //    var content = new RawHttpContent();
        //    using (var sr = new StreamReader(response.ContentStream))
        //    {
        //        content.Content = await sr.ReadToEndAsync();
        //    }
        //    content.IsStatusSuccessCode = response.IsStatusSuccessCode;
        //    content.StatusCode = response.StatusCode;

        //    return content;
        //}
    }
}