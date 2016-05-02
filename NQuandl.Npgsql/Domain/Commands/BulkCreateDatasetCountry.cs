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
//    public class BulkCreateDatasetCountries : IDefineCommand
//    {
//        public BulkCreateDatasetCountries(IObservable<DatasetCountry> datasetCountries)
//        {
//            DatasetCountries = datasetCountries;
//        }

//        public IObservable<DatasetCountry> DatasetCountries { get; }
//    }

//    public class HandleBulkCreateDatasetCountries : IHandleCommand<BulkCreateDatasetCountries>
//    {
//        private readonly IConfigureConnection _configuration;
//        private readonly IMapDataRecordToEntity<DatasetCountry> _mapper;


//        public HandleBulkCreateDatasetCountries([NotNull] IConfigureConnection configuration,
//            [NotNull] IMapDataRecordToEntity<DatasetCountry> mapper)
//        {
//            if (configuration == null)
//                throw new ArgumentNullException(nameof(configuration));

//            if (mapper == null)
//                throw new ArgumentNullException(nameof(mapper));


//            _configuration = configuration;
//            _mapper = mapper;
//        }

//        public Task Handle(BulkCreateDatasetCountries command)
//        {
//            var connection = new NpgsqlConnection(_configuration.ConnectionString);
//            connection.Open();
//            var writer =
//                connection.BeginBinaryImport(
//                    $"COPY {_mapper.GetTableName()} ({_mapper.GetColumnNamesWithoutId()}) FROM STDIN (FORMAT BINARY)");

//            command.DatasetCountries.Subscribe(datasetCountry =>
//            {
//                writer.StartRow();

//                var datasetId = _mapper.GetDbColumnInfoAttributeByProperty(x => x.DatasetId);
//                writer.Write(datasetCountry.DatasetId, datasetId.DbType);

//                var iso31661Alpha3 = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Iso31661Alpha3);
//                writer.Write(datasetCountry.Iso31661Alpha3, iso31661Alpha3.DbType);

//                var association = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Association);
//                writer.Write(datasetCountry.Association, association.DbType);
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