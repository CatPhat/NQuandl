using System.Collections.Generic;
using System.Linq;
using Npgsql;
using NpgsqlTypes;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Services.Helpers;
using NQuandl.Npgsql.Services.Transactions;

namespace NQuandl.Npgsql.Services.Extensions
{
    public static class NpgsqlCommandExtensions
    {
        public static void AddNpsqlParameterIfValueNotNull(this List<NpgsqlParameter> parameters, string parameterName,
            object parameterValue, NpgsqlDbType parameterType)
        {
            if (parameterValue != null)
            {
                parameters.Add(CreateNpgsqlParameter(parameterName, parameterValue, parameterType));
            }
        }

        private static NpgsqlParameter CreateNpgsqlParameter(string parameterName, object parameterValue,
            NpgsqlDbType paramaterType)
        {
            return new NpgsqlParameter
            {
                ParameterName = parameterName,
                NpgsqlDbType = paramaterType,
                Value = parameterValue
            };
        }

      
    }
}