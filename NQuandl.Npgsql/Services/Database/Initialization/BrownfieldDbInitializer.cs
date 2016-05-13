using NQuandl.Npgsql.Api;

namespace NQuandl.Npgsql.Services.Database.Initialization
{
    public class BrownfieldDbInitializer : IDbInitializer
    {
       
        public void Intialize(IDb db)
        {
            // Do nothing since db already exists
        }
    }
}