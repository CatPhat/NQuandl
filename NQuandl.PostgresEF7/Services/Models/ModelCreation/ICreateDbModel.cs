using Microsoft.Data.Entity;

namespace NQuandl.PostgresEF7.Services.Models.ModelCreation
{
    public interface ICreateDbModel
    {
        void Create(ModelBuilder modelBuilder);
    }
}