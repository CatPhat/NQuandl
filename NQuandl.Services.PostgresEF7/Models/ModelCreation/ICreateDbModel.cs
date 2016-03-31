using Microsoft.Data.Entity;

namespace NQuandl.Services.PostgresEF7.Models.ModelCreation
{
    public interface ICreateDbModel
    {
        void Create(ModelBuilder modelBuilder);
    }
}