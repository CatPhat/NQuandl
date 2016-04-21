using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Domain.Commands
{
    public class BulkCreateCountries : IDefineCommand
    {
        public BulkCreateCountries(IObservable<Country> countries)
        {
            Countries = countries;
        }

        public IObservable<Country> Countries { get; }
    }

    public class HandleBulkCreateCountries : IHandleCommand<BulkCreateCountries>
    {
        private readonly IConfigureConnection _configuration;
        private readonly IMapDataRecordToEntity<Country> _mapper;


        public HandleBulkCreateCountries([NotNull] IConfigureConnection configuration,
            [NotNull] IMapDataRecordToEntity<Country> mapper)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));


            _configuration = configuration;
            _mapper = mapper;
        }

        public Task Handle(BulkCreateCountries command)
        {
            var connection = new NpgsqlConnection(_configuration.ConnectionString);
            connection.Open();
            var writer =
                connection.BeginBinaryImport(
                    $"COPY {_mapper.GetTableName()} ({_mapper.GetColumnNames()}) FROM STDIN (FORMAT BINARY)");


            command.Countries.Subscribe(country =>
            {
                writer.StartRow();

                var code = _mapper.GetDbColumnInfoAttributeByProperty(x => x.CountryName);
                writer.Write(country.CountryName, code.DbType);

                var databaseCode = _mapper.GetDbColumnInfoAttributeByProperty(x => x.CountryCodeIso31661Alpha3);
                writer.Write(country.CountryCodeIso31661Alpha3, databaseCode.DbType);

                var databaseId = _mapper.GetDbColumnInfoAttributeByProperty(x => x.CountryCodeIso31661Numeric);
                writer.Write(country.CountryCodeIso31661Numeric, databaseId.DbType);

                var description = _mapper.GetDbColumnInfoAttributeByProperty(x => x.CountryCodeIso31661Alpha2);
                writer.Write(country.CountryCodeIso31661Alpha2, description.DbType);

                var endDate = _mapper.GetDbColumnInfoAttributeByProperty(x => x.CountryFlagUrl);
                writer.Write(country.CountryFlagUrl, endDate.DbType);
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