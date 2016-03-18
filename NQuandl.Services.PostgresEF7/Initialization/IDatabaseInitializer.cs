using Microsoft.Data.Entity;

namespace NQuandl.Services.PostgresEF7.Initialization
{
    public interface IDatabaseInitializer<in TContext> where TContext : DbContext
    {
        /// <summary>
        ///     Executes the strategy to initialize the database for the given context.
        /// </summary>
        /// <param name="context">The context. </param>
        void InitializeDatabase(TContext context);
    }
}