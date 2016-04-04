using Microsoft.Data.Entity;
using NQuandl.Domain.Persistence.Entities;

namespace NQuandl.Services.PostgresEF7.Models.ModelCreation
{
    public class DefaultDbModelCreator : ICreateDbModel
    {
        public void Create(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RawResponse>().ForNpgsqlToTable("raw_response");

            modelBuilder.Entity<RawResponse>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<RawResponse>()
                .Property(x => x.ResponseContent).HasColumnType("jsonb");
            modelBuilder.Entity<RawResponse>()
                .Property(x => x.CreationDate).HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");


            modelBuilder.Entity<Database>().ForNpgsqlToTable("database");

            modelBuilder.Entity<Database>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Database>()
                .HasMany(x => x.Datasets)
                .WithOne(y => y.Database)
                .HasForeignKey(z => z.DatabaseId)
                .IsRequired();


            modelBuilder.Entity<Dataset>().ForNpgsqlToTable("dataset");

            modelBuilder.Entity<Dataset>()
                .HasKey(x => x.Id);
            

            modelBuilder.Entity<Dataset>()
                .HasMany(x => x.ColumnNames)
                .WithOne(y => y.Dataset)
                .HasForeignKey(z => z.DatasetId)
                .IsRequired();

            modelBuilder.Entity<DatasetColumnName>().HasKey(x => x.Id);
        }
    }
}