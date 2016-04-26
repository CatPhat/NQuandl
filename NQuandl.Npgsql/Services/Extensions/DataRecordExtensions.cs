using System.Data;

namespace NQuandl.Npgsql.Services.Extensions
{
    public static class DataRecordExtensions
    {
        public static string GetStringOrDefault(this IDataRecord dataRecord, int index)
        {
            return dataRecord.IsDBNull(index) ? null : dataRecord.GetString(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRecord"></param>
        /// <param name="index"></param>
        /// <returns> Returns 0 if record value is null.</returns>
        public static int? GetInt32OrDefault(this IDataRecord dataRecord, int index)
        {
            int? value = null;
            return dataRecord.IsDBNull(index) ? value : dataRecord.GetInt32(index);
        }
    }
}