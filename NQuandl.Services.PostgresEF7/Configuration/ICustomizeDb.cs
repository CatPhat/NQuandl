using Microsoft.Data.Entity;

namespace NQuandl.Services.PostgresEF7.Configuration
{
    public interface ICustomizeDb
    {
        void Customize(DbContext dbContext);
    }
}