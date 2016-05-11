using System;
using System.Data;
using System.Reactive.Linq;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Transactions;

namespace NQuandl.Npgsql.Domain.Queries
{
    public class DataRecordsObservableBy : BaseDataRecordsQuery, IDefineQuery<IObservable<IDataRecord>> {}

    public class HandleDataRecordsObservableBy : IHandleQuery<DataRecordsObservableBy, IObservable<IDataRecord>>
    {
        private readonly IDb _db;
        private readonly ISqlMapper _sqlMapper;

        public HandleDataRecordsObservableBy([NotNull] ISqlMapper sqlMapper, [NotNull] IDb db)
        {
            if (sqlMapper == null)
                throw new ArgumentNullException(nameof(sqlMapper));
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            _sqlMapper = sqlMapper;
            _db = db;
        }

        public IObservable<IDataRecord> Handle(DataRecordsObservableBy query)
        {
            var sqlStatement = _sqlMapper.GetSelectSqlBy(query);
            return Observable.Create<IDataRecord>(async obs =>
            {
                using (var connection = _db.CreateConnection())
                using (var cmd = new NpgsqlCommand(sqlStatement, connection))
                {
                    await cmd.Connection.OpenAsync();
                    using (var reader = cmd.ExecuteReaderAsync())
                    {
                        var result = await reader;
                        while (await result.ReadAsync())
                        {
                            obs.OnNext(result);
                        }
                        obs.OnCompleted();
                    }
                    cmd.Connection.Close();
                }
            });
        }
    }
}