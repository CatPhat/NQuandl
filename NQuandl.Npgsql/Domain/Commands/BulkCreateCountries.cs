﻿using System;
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

                var name = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Name);
                writer.Write(country.Name, name.DbType);

                var databaseCode = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Iso31661Alpha3);
                writer.Write(country.Iso31661Alpha3, databaseCode.DbType);

                var databaseId = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Iso31661Numeric);
                writer.Write(country.Iso31661Numeric, databaseId.DbType);

                var description = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Iso31661Alpha2);
                writer.Write(country.Iso31661Alpha2, description.DbType);

                var countryFlagUrl = _mapper.GetDbColumnInfoAttributeByProperty(x => x.CountryFlagUrl);
                writer.Write(country.CountryFlagUrl, countryFlagUrl.DbType);

                var iso4217CurrencyAlphabeticCode = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Iso4217CurrencyAlphabeticCode);
                writer.Write(country.Iso4217CurrencyAlphabeticCode, iso4217CurrencyAlphabeticCode.DbType);

                var altName = _mapper.GetDbColumnInfoAttributeByProperty(x => x.AltName);
                writer.Write(country.CountryFlagUrl, altName.DbType);

                var iso4217CountryName = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Iso4217CountryName);
                writer.Write(country.Iso4217CountryName, iso4217CountryName.DbType);

                var iso4217MinorUnits = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Iso4217MinorUnits);
                writer.Write(country.Iso4217MinorUnits, iso4217MinorUnits.DbType);

                var iso4217CurrencyName = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Iso4217CurrencyName);
                writer.Write(country.Iso4217CurrencyName, iso4217CurrencyName.DbType);

                var iso4217CurrencyNumericCode = _mapper.GetDbColumnInfoAttributeByProperty(x => x.Iso4217CurrencyNumericCode);
                writer.Write(country.CountryFlagUrl, iso4217CurrencyNumericCode.DbType);
                
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