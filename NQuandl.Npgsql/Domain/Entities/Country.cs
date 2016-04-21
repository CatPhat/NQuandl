using NpgsqlTypes;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Domain.Entities
{
    [DbTableName("countries")]
    public class Country
    {
      
        [DbColumnInfo(1, "country_name", NpgsqlDbType.Text)]
        public string CountryName { get; set; }

        [DbColumnInfo(2, "country_code_iso_3166_1_alpha_3", NpgsqlDbType.Text)]
        public string CountryCodeIso31661Alpha3 { get; set; }

        [DbColumnInfo(3, "country_code_iso_3166_1_numeric", NpgsqlDbType.Integer)]
        public int CountryCodeIso31661Numeric { get; set; }

        [DbColumnInfo(4, "country_code_iso_3166_1_alpha_2", NpgsqlDbType.Text)]
        public string CountryCodeIso31661Alpha2 { get; set; }

        [DbColumnInfo(5, "country_flag_url", NpgsqlDbType.Text)]
        public string CountryFlagUrl { get; set; }
    }
}