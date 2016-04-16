using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;

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