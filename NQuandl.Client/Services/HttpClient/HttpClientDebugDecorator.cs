using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Client.Api.Quandl;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Services.HttpClient
{
    public class HttpClientDebugDecorator : IHttpClient
    {
        private readonly Func<IHttpClient> _httpFactory;


        public HttpClientDebugDecorator([NotNull] Func<IHttpClient> httpFactory)
        {
            if (httpFactory == null)
                throw new ArgumentNullException(nameof(httpFactory));

            _httpFactory = httpFactory;
        }


        public async Task<HttpClientResponse> GetAsync(string requestUri)
        {
            return await GetHttpClientResponseFromUri(requestUri);
        }


        private async Task GetStreamFromFile(string filePath, Stream stream)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                await fileStream.CopyToAsync(stream);
            }
            stream.Seek(0, SeekOrigin.Begin);
        }

        private async Task<HttpClientResponse> GetHttpClientResponseFromUri(string uri)
        {
            var fileName = GetFileNameFromUri(uri);
            var fullFilePathFromFileName = GetFullFilePathFromFileName(fileName, "json");

            if (CheckIfFileExists(fullFilePathFromFileName))
            {
                var stream = new MemoryStream();
                await GetStreamFromFile(fullFilePathFromFileName, stream);
                return new HttpClientResponse
                {
                    ContentStream = stream,
                    IsStatusSuccessCode = true,
                    StatusCode = "DEBUG OK"
                };
            }
            var debugNotFoundFilePath = GetFullFilePathFromFileName("debug_404.json", "json");
            var debugStream = new MemoryStream();

           //await GetStreamFromFile(debugNotFoundFilePath, debugStream);
            return new HttpClientResponse
            {
                ContentStream = debugStream,
                IsStatusSuccessCode = false,
                StatusCode = "DEBUG 404"
            };
        }

        private Task<MemoryStream> GetMemoryStreamFromBytes(byte[] bytes)
        {
            var memoryStream = new MemoryStream();
            memoryStream.WriteAsync(bytes, 0, bytes.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return Task.FromResult(memoryStream);
        }

        private static byte[] GetBytesFromFile(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        private string GetFileNameFromUri(string uri)
        {
            var fileName = uri;
            fileName = Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c, '_'));
            return fileName;
        }

        private string GetFullFilePathFromFileName(string fileName, string directory)
        {
            const string fileBasePath = @"C:\Users\USER9\Documents\quandl_data\output";
            return string.Format(fileBasePath + "\\" + directory + "\\" + fileName);
        }

        private bool CheckIfFileExists(string fullFileName)
        {
            return File.Exists(fullFileName);
        }

        private void WriteToFile(string response, string fullFileName)
        {
            File.WriteAllText(fullFileName, response);
        }
    }
}