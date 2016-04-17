using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Domain.Commands
{
    public class BulkCreateDatabases : IDefineCommand
    {
        public IObservable<Database> Databases { get; private set; }

        public BulkCreateDatabases([NotNull] IObservable<Database> databases)
        {
            if (databases == null)
                throw new ArgumentNullException(nameof(databases));
            Databases = databases;
        }
    }

    public class HandleBulkCreateDatabases : IHandleCommand<BulkCreateDatabases>
    {
        private readonly IConfigureConnection _configuration;
        private readonly IMapDataRecordToEntity<Database> _mapper;


        public HandleBulkCreateDatabases([NotNull] IConfigureConnection configuration,
            [NotNull] IMapDataRecordToEntity<Database> mapper)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));
            _configuration = configuration;
            _mapper = mapper;
        }

        public Task Handle(BulkCreateDatabases command)
        {
            var connection = new NpgsqlConnection(_configuration.ConnectionString);
            connection.Open();
            var writer =
                connection.BeginBinaryImport(
                    $"COPY {_mapper.GetTableName()} ({_mapper.GetColumnNames()}) FROM STDIN (FORMAT BINARY)");

            command.Databases.Subscribe(database =>
            {
                writer.StartRow();

                var id = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Id);
                writer.Write(database.Id, id.DbType);

                var databaseCode = _mapper.GetDbColumnInfoAttributeByProperty(x => x.DatabaseCode);
                writer.Write(database.DatabaseCode, databaseCode.DbType);

                var datasetsCount = _mapper.GetDbColumnInfoAttributeByProperty(x => x.DatasetsCount);
                writer.Write(database.DatasetsCount, datasetsCount.DbType);

                var description = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Description);
                writer.Write(database.Description, description.DbType);

                var downloads = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Downloads);
                writer.Write(database.Downloads, downloads.DbType);

                var image = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Image);
                writer.Write(database.Image, image.DbType);

                var name = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Name);
                writer.Write(database.Name, name.DbType);

                var premium = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Premium);
                writer.Write(database.Premium, premium.DbType);

             
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
