using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Quandl;
using NQuandl.Api.Quandl.Helpers;
using NQuandl.Api.Transactions;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Domain.Quandl.Queries
{
    public class GetAllDatabaseListsBy : IDefineQuery<Task<IEnumerable<Databases>>>
    {
    }

    public class HandleGetAllDatabaseListsBy : IHandleQuery<GetAllDatabaseListsBy, Task<IEnumerable<Databases>>>
    {
        private readonly IQuandlClient _client;

        public HandleGetAllDatabaseListsBy([NotNull] IQuandlClient client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            _client = client;
        }

        public async Task<IEnumerable<Databases>> Handle(GetAllDatabaseListsBy query)
        {
            var databaseListQuery1 = new DatabaseListBy {Page = 1};
            var databaseListResponse1 =
                await _client.GetAsync<DatabaseList>(databaseListQuery1.ToQuandlClientRequestParameters());

            var databaseList = new List<Databases>();
            for (var i = 2; i <= databaseListResponse1.meta.total_pages; i++)
            {
                var response =
                    await
                        _client.GetAsync<DatabaseList>(new DatabaseListBy {Page = i}.ToQuandlClientRequestParameters());

                databaseList.AddRange(response.databases);
            }
            
            return databaseList;
        }
    }
}