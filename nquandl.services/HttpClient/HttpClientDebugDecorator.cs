using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Quandl;
using NQuandl.Api.Quandl.Helpers;
using NQuandl.Domain.Quandl.RequestParameters;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Services.HttpClient
{
    public class HttpClientDebugDecorator : IHttpClient
    {
        private readonly Func<IHttpClient> _httpFactory;
        private readonly IHttpResponseCache _cache;

        public HttpClientDebugDecorator([NotNull] Func<IHttpClient> httpFactory, [NotNull] IHttpResponseCache cache)
        {
            if (httpFactory == null) throw new ArgumentNullException(nameof(httpFactory));
            if (cache == null) throw new ArgumentNullException(nameof(cache));
            _httpFactory = httpFactory;
            _cache = cache;
        }

     

        public async Task<HttpClientResponse> GetAsync(string requestUri)
        {
            return await GetHttpClientResponseFromUri(requestUri);
        }

     
        private Task<FileStream> GetStreamFromFile(string filePath)
        {
          
            var stream = File.OpenRead(filePath);

            return Task.FromResult(stream);
        }

        private async Task<HttpClientResponse> GetHttpClientResponseFromUri(string uri)
        {
            var fileName = GetFileNameFromUri(uri);
            var fullFilePathFromFileName = GetFullFilePathFromFileName(fileName, "json");

            if (CheckIfFileExists(fullFilePathFromFileName))
            {
                var stream = await GetStreamFromFile(fullFilePathFromFileName);
                return new HttpClientResponse
                {
                    ContentStream = stream,
                    IsStatusSuccessCode = true,
                    StatusCode = "DEBUG OK"
                };
            }
            var debugNotFoundFilePath = GetFullFilePathFromFileName("debug_404.json", "json");
            var debugStream = await GetStreamFromFile(debugNotFoundFilePath);
            return new HttpClientResponse
            {
                ContentStream = debugStream,
                IsStatusSuccessCode = false,
                StatusCode = "DEBUG 404"
            };
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
