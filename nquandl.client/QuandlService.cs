﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using NQuandl.Client.Interfaces;

namespace NQuandl.Client
{
    public class QuandlService : IQuandlService
    {
        private readonly Uri _baseUri;

        public QuandlService(string baseUrl)
        {
            _baseUri = new Uri(baseUrl);
        }
        
        public async Task<string> GetStringAsync(IQuandlRequest request)
        {
            var httpClient = new HttpClient();
            var uri = new Uri(_baseUri + "/" + request.Uri.Uri);
            return await httpClient.GetStringAsync(uri);
        }
    }
}