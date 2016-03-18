using Microsoft.Data.Entity;

namespace NQuandl.Services.PostgresEF7.Initialization
{
    public class BrownfieldDbInitializer : IDatabaseInitializer<DbContext>
    {
        public void InitializeDatabase(DbContext db)
        {
            // assume db already exists and should not be fooled with
        }
    }
}