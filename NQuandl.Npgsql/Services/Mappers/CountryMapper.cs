﻿using System.Data;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Services.Mappers
{
    public class CountryMapper : BaseDataRecordMapper<Country>
    {
        public override Country ToEntity(IDataRecord record)
        {
            return new Country
            {
                Name =
                    record.GetStringOrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(Country.Name))),
                Iso31661Alpha3 =
                    record.GetStringOrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(Country.Iso31661Alpha3))),
                Iso31661Alpha2 =
                    record.GetStringOrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(Country.Iso31661Alpha2))),
                Iso31661Numeric =
                    record.GetInt32(AttributeMetadata.GetColumnIndexByPropertName(nameof(Country.Iso31661Numeric))),
                CountryFlagUrl =
                    record.GetStringOrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(Country.CountryFlagUrl))),
                AltName =
                    record.GetStringOrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(Country.AltName))),
                Iso4217CurrencyAlphabeticCode =
                    record.GetStringOrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(Country.Iso4217CurrencyAlphabeticCode))),
                Iso4217CountryName =
                    record.GetStringOrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(Country.Iso4217CountryName))),
                Iso4217MinorUnits =
                    record.GetInt32(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(Country.Iso4217MinorUnits))),
                Iso4217CurrencyName =
                    record.GetStringOrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(Country.Iso4217CurrencyName))),
                Iso4217CurrencyNumericCode =
                    record.GetInt32(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(Country.Iso4217CurrencyNumericCode)))
            };
        }
    }
}