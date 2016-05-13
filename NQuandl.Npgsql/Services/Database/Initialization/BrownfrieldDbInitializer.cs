using NQuandl.Npgsql.Api;

namespace NQuandl.Npgsql.Services.Database.Initialization
{
    public class BrownfrieldDbInitializer : IDbInitializer
    {
       
        public void Intialize(IDb db)
        {
            // Do nothing since db already exists
        }
    }
}