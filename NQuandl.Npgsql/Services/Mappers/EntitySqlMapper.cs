//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JetBrains.Annotations;
//using NQuandl.Npgsql.Api.DTO;
//using NQuandl.Npgsql.Api.Entities;
//using NQuandl.Npgsql.Api.Metadata;
//using NQuandl.Npgsql.Api.Transactions;
//using NQuandl.Npgsql.Services.Extensions;

//namespace NQuandl.Npgsql.Services.Mappers
//{
//    public class EntitySqlMapper<TEntity> : IEntitySqlMapper<TEntity> where TEntity : DbEntity
//    {
//        private readonly IEntityMetadataCache<TEntity> _metadata;
//        private readonly ISqlMapper _sqlMapper;

//        public EntitySqlMapper([NotNull] ISqlMapper sqlMapper, [NotNull] IEntityMetadataCache<TEntity> metadata)
//        {
//            if (sqlMapper == null)
//                throw new ArgumentNullException(nameof(sqlMapper));
//            if (metadata == null)
//                throw new ArgumentNullException(nameof(metadata));
//            _sqlMapper = sqlMapper;
//            _metadata = metadata;
//        }

//        public string GetBulkInsertSql()
//        {
//            return _sqlMapper.GetBulkInsertSql(_metadata.GetTableName(), GetOrderedColumnsStrings());
//        }

//        public string GetInsertSql(IEnumerable<DbInsertData> dbDatas)
//        {
//            return _sqlMapper.GetInsertSql(_metadata.GetTableName(), GetOrderedColumnsStrings(), dbDatas);
//        }

//        private string[] GetOrderedColumnsStrings()
//        {
//            return _metadata.ToColumnNameWithIndices().Select(x => x.ColumnName).ToArray();
//        }
//    }
//}