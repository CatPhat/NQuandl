//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Reactive.Linq;
//using System.Threading.Tasks;
//using JetBrains.Annotations;
//using NQuandl.Npgsql.Api;
//using NQuandl.Npgsql.Api.Entities;
//using NQuandl.Npgsql.Api.Mappers;
//using NQuandl.Npgsql.Api.Metadata;
//using NQuandl.Npgsql.Api.Transactions;
//using NQuandl.Npgsql.Services.Mappers;

//namespace NQuandl.Npgsql.Services.Transactions
//{
//    public class EntityReader<TEntity> : IReadEntities<TEntity> where TEntity : DbEntity
//    {
//        private readonly IEntitySqlObjectMapper<TEntity> _objectMapper;
//        private readonly IEntitySqlMapper _sqlMapper;
//        private readonly IDb _db;
    

//        public EntityReader([NotNull] IEntitySqlObjectMapper<TEntity> objectMapper, [NotNull] IEntitySqlMapper sqlMapper, [NotNull] IDb db)
//        {
//            if (objectMapper == null)
//                throw new ArgumentNullException(nameof(objectMapper));
//            if (sqlMapper == null)
//                throw new ArgumentNullException(nameof(sqlMapper));

//            _objectMapper = objectMapper;
//            _sqlMapper = sqlMapper;
//            _db = db;
//        }

//        public async Task<TEntity> GetAsync(EntitiesReaderQuery<TEntity> entityQuery)
//        {
//            var query = _objectMapper.GetEntitiesReaderQuery(entityQuery);
//            var result = await _db.ExecuteQuery(_sqlMapper.GetSelectSqlBy(query, _sqlMapper.))


//            return result;
//        }




     

     
//    }
//}