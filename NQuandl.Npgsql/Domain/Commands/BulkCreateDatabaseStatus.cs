//using System;
//using System.Threading.Tasks;
//using JetBrains.Annotations;
//using Npgsql;
//using NQuandl.Npgsql.Api;
//using NQuandl.Npgsql.Api.Transactions;
//using NQuandl.Npgsql.Domain.Entities;
//using NQuandl.Npgsql.Services.Extensions;

//namespace NQuandl.Npgsql.Domain.Commands
//{
//    public class BulkCreateDatabaseStatuses : IDefineCommand
//    {
//        public BulkCreateDatabaseStatuses(IObservable<DatabaseStatus> databaseStatuses)
//        {
//            DatabaseStatuses = databaseStatuses;
//        }

//        public IObservable<DatabaseStatus> DatabaseStatuses { get; }
//    }

//    public class HandleBulkCreateDatabaseStatus : IHandleCommand<BulkCreateDatabaseStatuses>
//    {
//        private readonly IConfigureConnection _configuration;
//        private readonly IMapDataRecordToEntity<DatabaseStatus> _mapper;


//        public HandleBulkCreateDatabaseStatus([NotNull] IConfigureConnection configuration,
//            [NotNull] IMapDataRecordToEntity<DatabaseStatus> mapper)
//        {
//            if (configuration == null)
//                throw new ArgumentNullException(nameof(configuration));

//            if (mapper == null)
//                throw new ArgumentNullException(nameof(mapper));


//            _configuration = configuration;
//            _mapper = mapper;
//        }

//        public Task Handle(BulkCreateDatabaseStatuses command)
//        {
//            var connection = new NpgsqlConnection(_configuration.ConnectionString);
//            connection.Open();
//            var writer =
//                connection.BeginBinaryImport("SQL STATEMENT GOES HEREE!!!!");

//            command.DatabaseStatuses.Subscribe(DatabaseStatus =>
//            {
//                writer.StartRow();

                
//            },
//                onCompleted: () => DisposeConnectionAndWrite(connection, writer),
//                onError:
//                    exception => { throw new Exception(exception.Message); });

//            return Task.FromResult(0);
//        }

//        private static void DisposeConnectionAndWrite(NpgsqlConnection connection, NpgsqlBinaryImporter importer)
//        {
//            importer.Close();
//            importer.Dispose();
//            connection.Dispose();
//        }
//    }
//}