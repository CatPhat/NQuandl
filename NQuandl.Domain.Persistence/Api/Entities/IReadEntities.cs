using System;
using System.Linq;
using System.Linq.Expressions;

namespace NQuandl.Domain.Persistence.Api.Entities
{
    /// <summary>
    /// Informs an underlying relational data store to return sets of read-only entity instances.
    /// </summary>
    public interface IReadEntities
    {
        /// <summary>
        /// Inform an underlying relational data store to return a set of read-only entity instances.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity instances that the underlying relational data
        /// store should return.</typeparam>
        /// <returns>IQueryable for set of read-only TEntity instances from an underlying relational
        /// data store.</returns>
        IQueryable<TEntity> Query<TEntity>() where TEntity : Entity;

    }
}