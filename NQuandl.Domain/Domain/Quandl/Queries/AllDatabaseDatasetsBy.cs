using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Api.Transactions;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Domain.Quandl.Queries
{
    public class AllDatabaseDatasetsBy : IDefineQuery<Task<IEnumerable<Dataset>>>
    {

    }
}
