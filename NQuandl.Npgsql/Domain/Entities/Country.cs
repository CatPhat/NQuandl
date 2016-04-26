using NpgsqlTypes;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Domain.Entities
{
    [DbTableName("countries")]
    public class Country
    {
      
        [DbColumnInfo(0, "name", NpgsqlDbType.Text)]
        public string Name { get; set; }

        [DbColumnInfo(1, "iso31661alpha3", NpgsqlDbType.Text)]
        public string Iso31661Alpha3 { get; set; }

        [DbColumnInfo(2, "iso31661numeric", NpgsqlDbType.Integer)]
        public int? Iso31661Numeric { get; set; }

        [DbColumnInfo(3, "iso31661alpha2", NpgsqlDbType.Text)]
        public string Iso31661Alpha2 { get; set; }

        [DbColumnInfo(4, "country_flag_url", NpgsqlDbType.Text)]
        public string CountryFlagUrl { get; set; }

        [DbColumnInfo(5, "altname", NpgsqlDbType.Text)]
        public string AltName { get; set; }

        [DbColumnInfo(6, "iso4217_currency_alphabetic_code", NpgsqlDbType.Text)]
        public string Iso4217CurrencyAlphabeticCode { get; set; }

        [DbColumnInfo(7, "iso4217_country_name", NpgsqlDbType.Text)]
        public string Iso4217CountryName { get; set; }

        [DbColumnInfo(8, "iso4217_minor_units", NpgsqlDbType.Integer)]
        public int? Iso4217MinorUnits { get; set; }

        [DbColumnInfo(9, "iso4217_currency_name", NpgsqlDbType.Text)]
        public string Iso4217CurrencyName { get; set; }

        [DbColumnInfo(10, "iso4217_currency_numeric_code", NpgsqlDbType.Integer)]
        public int? Iso4217CurrencyNumericCode { get; set; }
    }
}