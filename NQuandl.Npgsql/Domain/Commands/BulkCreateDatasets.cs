using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Domain.Commands
{
    public class BulkCreateDatasets : IDefineCommand
    {
        public BulkCreateDatasets(IObservable<Dataset> datasets)
        {
            Datasets = datasets;
        }

        public IObservable<Dataset> Datasets { get; }
    }

    public class HandleBulkCreateDatasets : IHandleCommand<BulkCreateDatasets>
    {
        private readonly IConfigureConnection _configuration;
        private readonly IMapDataRecordToEntity<Dataset> _mapper;


        public HandleBulkCreateDatasets([NotNull] IConfigureConnection configuration,
            [NotNull] IMapDataRecordToEntity<Dataset> mapper)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));


            _configuration = configuration;
            _mapper = mapper;
        }

        public Task Handle(BulkCreateDatasets command)
        {
            var connection = new NpgsqlConnection(_configuration.ConnectionString);
            connection.Open();
            var writer =
                connection.BeginBinaryImport(
                    $"COPY {_mapper.GetTableName()} ({_mapper.GetColumnNames()}) FROM STDIN (FORMAT BINARY)");


            command.Datasets.Subscribe(dataset =>
            {
                writer.StartRow();

                var id = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Id);
                writer.Write(dataset.Id, id.DbType);

                var code = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Code);
                writer.Write(dataset.Code, code.DbType);

                var databaseCode = _mapper.GetDbColumnInfoAttributeByProperty(x => x.DatabaseCode);
                writer.Write(dataset.DatabaseCode, databaseCode.DbType);

                var databaseId = _mapper.GetDbColumnInfoAttributeByProperty(x => x.DatabaseId);
                writer.Write(dataset.DatabaseId, databaseId.DbType);

                var description = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Description);
                writer.Write(dataset.Description, description.DbType);

                var endDate = _mapper.GetDbColumnInfoAttributeByProperty(x => x.EndDate);
                writer.Write(dataset.EndDate, endDate.DbType);

                var frequency = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Frequency);
                writer.Write(dataset.Frequency, frequency.DbType);

                var name = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Name);
                writer.Write(dataset.Name, name.DbType);

                var refreshedAt = _mapper.GetDbColumnInfoAttributeByProperty(x => x.RefreshedAt);
                writer.Write(dataset.RefreshedAt, refreshedAt.DbType);

                var startDate = _mapper.GetDbColumnInfoAttributeByProperty(x => x.StartDate);
                writer.Write(dataset.StartDate, startDate.DbType);

                var data = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Data);
                writer.Write(dataset.Data, data.DbType);

                
            },
                onCompleted: () => DisposeConnectionAndWrite(connection, writer),
                onError:
                    exception => { throw new Exception(exception.Message); });

            return Task.FromResult(0);
        }

        private static void DisposeConnectionAndWrite(NpgsqlConnection connection, NpgsqlBinaryImporter importer)
        {
         
            importer.Close();
            importer.Dispose();
            connection.Dispose();
        }
    }
}