using System.Data;
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
              
                CountryName = 
                    record.GetStringOrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(Country.CountryName))),
                CountryCodeIso31661Alpha3 = 
                    record.GetStringOrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(Country.CountryCodeIso31661Alpha3))),
                CountryCodeIso31661Alpha2 = 
                    record.GetStringOrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(Country.CountryCodeIso31661Alpha2))),
                CountryCodeIso31661Numeric = record.GetInt32(AttributeMetadata.GetColumnIndexByPropertName(nameof(Country.CountryCodeIso31661Numeric))),
                CountryFlagUrl = 
                    record.GetStringOrDefault(
                        AttributeMetadata.GetColumnIndexByPropertName(nameof(Country.CountryFlagUrl)))
            };
        }
    }
}