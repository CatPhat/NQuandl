using System;
using System.Collections.Generic;
using System.Data;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Transactions;

namespace NQuandl.Npgsql.Domain.Queries
{
    public class DataRecordsEnumerableBy : BaseDataRecordsQuery, IDefineQuery<IEnumerable<IDataRecord>>
    {
        
    }

    public class HandleDataRecordsEnumerableBy : IHandleQuery<DataRecordsEnumerableBy, IEnumerable<IDataRecord>>
    {
        private readonly IDb _db;
        private readonly ISqlMapper _sqlMapper;

        public HandleDataRecordsEnumerableBy([NotNull] ISqlMapper sqlMapper, [NotNull] IDb db)
        {
            if (sqlMapper == null)
                throw new ArgumentNullException(nameof(sqlMapper));
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            _sqlMapper = sqlMapper;
            _db = db;
        }

        public IEnumerable<IDataRecord> Handle(DataRecordsEnumerableBy query)
        {
            var sqlStatement = _sqlMapper.GetSelectSqlBy(query);
            using (var connection = _db.CreateConnection())
            using (var cmd = new NpgsqlCommand(sqlStatement, connection))
            {
                cmd.Connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return reader;
                    }
                }
                cmd.Connection.Close();
            }
        }
    }
}