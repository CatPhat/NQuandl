using System;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Extensions;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Domain.Queries
{
    public class RawResponseBy : IDefineQuery<Task<RawResponse>>
    {
        public RawResponseBy(string requestUri)
        {
            RequestUri = requestUri;
        }

        public string RequestUri { get; }
    }

    public class HandleRawResposneBy : IHandleQuery<RawResponseBy, Task<RawResponse>>
    {
        private readonly IMapDataRecordToEntity<RawResponse> _mapper;
        private readonly IExecuteRawSql _sql;

        public HandleRawResposneBy([NotNull] IExecuteRawSql sql, [NotNull] IMapDataRecordToEntity<RawResponse> mapper)
        {
            if (sql == null)
                throw new ArgumentNullException(nameof(sql));
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));
            _sql = sql;
            _mapper = mapper;
        }

        public async Task<RawResponse> Handle(RawResponseBy query)
        {
            var queryString = new StringBuilder();
            queryString.Append($"select {_mapper.GetColumnNames()} " +
                               $"from {_mapper.GetTableName()} " +
                               $"where {_mapper.GetColumnNameByPropertyName(x => x.RequestUri)} " +
                               $"= '{query.RequestUri}'");

            var result = _sql.ExecuteQueryAsync(queryString.ToString());
            var task = result.FirstOrDefaultAsync();
            return _mapper.ToEntity(await task);
        }
    }
}