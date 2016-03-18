using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NQuandl.Api.Entities
{
    /// <summary>
    ///     Synchronizes data state changes with an underlying data store.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        ///     Commit all current data changes to the underlying data store.
        /// </summary>
        /// <returns>
        ///     The number of data units whose values were modified after saving
        ///     changes.
        /// </returns>
        // ReSharper disable UnusedMethodReturnValue.Global
        int SaveChanges();

        // ReSharper restore UnusedMethodReturnValue.Global

        /// <summary>
        ///     Asynchronously commit all current data changes to the underlying data store.
        /// </summary>
        /// <returns>
        ///     A task result that contains the number of data units whose values were modified after saving
        ///     changes.
        /// </returns>
        Task<int> SaveChangesAsync();
    }
}