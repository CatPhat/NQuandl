using System;
using System.Reactive.Linq;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Entities;

namespace NQuandl.Npgsql.Domain.Queries
{
    public class DatabaseDatasetBy : IDefineQuery<IObservable<DatabaseDataset>>
    {
        public DatabaseDatasetBy(string quandlCode)
        {
            QuandlCode = quandlCode;
        }

        public DatabaseDatasetBy(string databaseCode, string datasetCode)
        {
            DatabaseCode = databaseCode;
            DatasetCode = datasetCode;
        }

        public string DatabaseCode { get; set; }
        public string DatasetCode { get; set; }
        public string QuandlCode { get; set; }
    }

    public class HandleDatabaseDatasetBy : IHandleQuery<DatabaseDatasetBy, IObservable<DatabaseDataset>>
    {
        private readonly IMapDataRecordToEntity<DatabaseDataset> _mapper;
        private readonly IExecuteRawSql _sql;

        public HandleDatabaseDatasetBy([NotNull] IExecuteRawSql sql,
            [NotNull] IMapDataRecordToEntity<DatabaseDataset> mapper)
        {
            if (sql == null)
                throw new ArgumentNullException(nameof(sql));
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));
            _sql = sql;
            _mapper = mapper;
        }

        public IObservable<DatabaseDataset> Handle(DatabaseDatasetBy query)
        {
            var quandlCode = !string.IsNullOrEmpty(query.QuandlCode)
                ? query.QuandlCode
                : $"{query.DatabaseCode}/{query.DatabaseCode}";

            quandlCode = quandlCode.ToUpperInvariant();
            var queryString = $"select * from {_mapper.AttributeMetadata.TableName} " +
                              $"where {_mapper.AttributeMetadata.PropertyNameAttributeDictionary[nameof(DatabaseDataset.QuandlCode)]} " +
                              $"= {quandlCode}";
            var response = _sql.ExecuteQueryAsync(queryString);

            return Observable.Create<DatabaseDataset>(
                obs =>
                    response.Subscribe(record =>
                    {
                        var databaseDataset = _mapper.ToEntity(record);
                        obs.OnNext(databaseDataset);
                        obs.OnCompleted();
                    }));
        }
    }
}