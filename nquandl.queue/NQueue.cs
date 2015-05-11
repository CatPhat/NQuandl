﻿using System.Collections.Generic;
using System.Threading.Tasks;
using NQuandl.Client.Entities.Base;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;
using NQuandl.Client.Responses;
using SimpleInjector;

namespace NQuandl.Queue
{
    public static class NQueue
    {
        private static readonly Container _container;

        static NQueue()
        {
            _container = new Container();
            _container.ComposeRoot();
            _container.Verify();
        }

        public static async Task<DeserializedEntityResponse<TEntity>> GetAsync<TEntity>(
       RequestParameterOptions options = null)
       where TEntity : QuandlEntity, new()
        {
            var instance = _container.GetInstance<IJsonServiceQueue>();
            return await instance.GetAsync<TEntity>(options);
        }

        public static async Task<IEnumerable<DeserializedEntityResponse<TEntity>>> GetAsync<TEntity>(
          List<QueueRequest<TEntity>> requests) where TEntity : QuandlEntity, new()
        {
            var instance = _container.GetInstance<IJsonServiceQueue>();
            return await instance.GetAsync(requests);
        }

        public static QueueStatus GetQueueStatus()
        {
            var logger =  _container.GetInstance<IQueueStatusLogger>();
            return logger.Status;
        }
    }
}