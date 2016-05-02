//using System;
//using System.Data;
//using System.Linq;
//using System.Reactive.Linq;
//using System.Reactive.Threading.Tasks;
//using System.Threading.Tasks;
//using JetBrains.Annotations;
//using NQuandl.Npgsql.Api;
//using NQuandl.Npgsql.Api.Transactions;
//using NQuandl.Npgsql.Domain.Entities;
//using NQuandl.Npgsql.Services.Extensions;
//using NQuandl.Npgsql.Services.Helpers;

//namespace NQuandl.Npgsql.Domain.Queries
//{
//    public class DatabaseDatasetBy : IDefineQuery<Task<DatabaseDataset>>
//    {
//        public DatabaseDatasetBy(string quandlCode)
//        {
//            QuandlCode = quandlCode;
//        }

//        public DatabaseDatasetBy(string databaseCode, string datasetCode)
//        {
//            DatabaseCode = databaseCode;
//            DatasetCode = datasetCode;
//        }

//        public string DatabaseCode { get; set; }
//        public string DatasetCode { get; set; }
//        public string QuandlCode { get; set; }
//    }

//    public class HandleDatabaseDatasetBy : IHandleQuery<DatabaseDatasetBy, Task<DatabaseDataset>>
//    {
//        private readonly IMapDataRecordToEntity<DatabaseDataset> _mapper;
//        private readonly IExecuteRawSql _sql;

//        public HandleDatabaseDatasetBy([NotNull] IExecuteRawSql sql,
//            [NotNull] IMapDataRecordToEntity<DatabaseDataset> mapper)
//        {
//            if (sql == null)
//                throw new ArgumentNullException(nameof(sql));
//            if (mapper == null)
//                throw new ArgumentNullException(nameof(mapper));
//            _sql = sql;
//            _mapper = mapper;
//        }

//        public async Task<DatabaseDataset> Handle(DatabaseDatasetBy query)
//        {
//            var quandlCode = !string.IsNullOrEmpty(query.QuandlCode)
//                ? query.QuandlCode
//                : $"{query.DatabaseCode}/{query.DatabaseCode}";

//            quandlCode = quandlCode.ToUpperInvariant();
//            var columnNames = _mapper.GetColumnNames();
//            var queryString = $"select {columnNames} " +
//                              $"from {_mapper.GetTableName()} " +
//                              $"where {_mapper.GetColumnNameByPropertyName(dataset => dataset.QuandlCode)} " +
//                              $"= '{quandlCode}'";
//            var response = _sql.ExecuteQueryAsync(queryString);

//            var task = await response.FirstOrDefaultAsync();
//            return _mapper.ToEntity(task);
//        }


//    }
//}