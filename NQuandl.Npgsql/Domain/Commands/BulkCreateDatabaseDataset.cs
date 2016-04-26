using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Client.Domain.Responses;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Domain.Commands
{
    public class BulkCreateDatabaseDataset : IDefineCommand
    {
        public BulkCreateDatabaseDataset(IObservable<DatabaseDataset> databaseDatasetsObservable)
        {
            DatabaseDatasetsObservable = databaseDatasetsObservable;
        }

        public BulkCreateDatabaseDataset(IEnumerable<CsvDatabaseDataset> databaseDatasetsEnumerable)
        {
            DatabaseDatasetsEnumerable = databaseDatasetsEnumerable;
        }

        public IEnumerable<CsvDatabaseDataset> DatabaseDatasetsEnumerable { get; }
        public IObservable<DatabaseDataset> DatabaseDatasetsObservable { get; }
    }

    public class HandleBulkCreateDatabaseDataset : IHandleCommand<BulkCreateDatabaseDataset>
    {
        private readonly IConfigureConnection _configuration;
        private readonly IMapDataRecordToEntity<DatabaseDataset> _mapper;


        public HandleBulkCreateDatabaseDataset([NotNull] IConfigureConnection configuration,
            [NotNull] IMapDataRecordToEntity<DatabaseDataset> mapper)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));


            _configuration = configuration;
            _mapper = mapper;
        }

        public Task Handle(BulkCreateDatabaseDataset command)
        {
            var connection = new NpgsqlConnection(_configuration.ConnectionString);
            connection.Open();
            var writer =
                connection.BeginBinaryImport(
                    $"COPY {_mapper.GetTableName()} ({_mapper.GetColumnNamesWithoutId()}) FROM STDIN (FORMAT BINARY)");

            IObservable<DatabaseDataset> databaseDatasetsObservable;
            if (command.DatabaseDatasetsEnumerable != null)
            {
                var observable = command.DatabaseDatasetsEnumerable.ToObservable();
                databaseDatasetsObservable = Observable.Create<DatabaseDataset>(observer =>
                    observable.Subscribe(dataset => observer.OnNext(ToDatabaseDataset(dataset)),
                        onCompleted: observer.OnCompleted, onError:
                            exception => { throw new Exception(exception.Message); }));
            }
            else
            {
                databaseDatasetsObservable = command.DatabaseDatasetsObservable;
            }

            databaseDatasetsObservable.Subscribe(databaseDataset =>
            {
                writer.StartRow();

                var databaseCode = _mapper.GetDbColumnInfoAttributeByProperty(x => x.DatabaseCode);
                writer.Write(databaseDataset.DatabaseCode, databaseCode.DbType);

                var datasetCode = _mapper.GetDbColumnInfoAttributeByProperty(x => x.DatasetCode);
                writer.Write(databaseDataset.DatasetCode, datasetCode.DbType);

                var quandlCode = _mapper.GetDbColumnInfoAttributeByProperty(x => x.QuandlCode);
                writer.Write(databaseDataset.QuandlCode, quandlCode.DbType);

                var description = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Description);
                writer.Write(databaseDataset.Description, description.DbType);
            },
                onCompleted: () => DisposeConnectionAndWrite(connection, writer),
                onError:
                    exception => { throw new Exception(exception.Message); });

            return Task.FromResult(0);
        }

        private static DatabaseDataset ToDatabaseDataset(CsvDatabaseDataset csvDatabaseDataset)
        {
            return new DatabaseDataset
            {
                DatabaseCode = csvDatabaseDataset.DatabaseCode,
                DatasetCode = csvDatabaseDataset.DatasetCode,
                Description = csvDatabaseDataset.DatasetDescription,
                QuandlCode = csvDatabaseDataset.QuandlCode
            };
        }

        private static void DisposeConnectionAndWrite(NpgsqlConnection connection, NpgsqlBinaryImporter importer)
        {
            importer.Close();
            importer.Dispose();
            connection.Dispose();
        }
    }
}