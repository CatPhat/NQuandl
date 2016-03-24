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
    public class GetAllDatabaseListsBy : IDefineQuery<Task<IEnumerable<DatabaseList>>>
    {
    }

    public class HandleGetAllDatabaseListsBy : IHandleQuery<GetAllDatabaseListsBy, Task<IEnumerable<DatabaseList>>>
    {
        private readonly IQuandlClient _client;

        public HandleGetAllDatabaseListsBy([NotNull] IQuandlClient client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            _client = client;
        }

        public async Task<IEnumerable<DatabaseList>> Handle(GetAllDatabaseListsBy query)
        {
            var databaseListQuery1 = new DatabaseListBy {Page = 1};
            var databaseListResponse1 =
                await _client.GetAsync<DatabaseList>(databaseListQuery1.ToQuandlClientRequestParameters());

            var databaseLists = new List<DatabaseList> {databaseListResponse1};
            for (var i = 2; i <= databaseListResponse1.meta.total_pages; i++)
            {
                var response =
                    await
                        _client.GetAsync<DatabaseList>(new DatabaseListBy {Page = i}.ToQuandlClientRequestParameters());

                databaseLists.Add(response);
            }

            return databaseLists;
        }
    }
}