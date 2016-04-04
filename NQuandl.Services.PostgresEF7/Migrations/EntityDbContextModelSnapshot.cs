using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using NQuandl.Services.PostgresEF7;

namespace NQuandl.Services.PostgresEF7.Migrations
{
    [DbContext(typeof(EntityDbContext))]
    partial class EntityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("NQuandl.Domain.Persistence.Entities.Database", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DatabaseCode");

                    b.Property<int>("DatasetsCount");

                    b.Property<string>("Description");

                    b.Property<long>("Downloads");

                    b.Property<string>("Image");

                    b.Property<string>("Name");

                    b.Property<bool>("Premium");

                    b.HasKey("Id");

                    b.HasAnnotation("Npgsql:TableName", "database");
                });

            modelBuilder.Entity("NQuandl.Domain.Persistence.Entities.Dataset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Data");

                    b.Property<string>("DatabaseCode");

                    b.Property<int>("DatabaseId");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Frequency");

                    b.Property<string>("Name");

                    b.Property<DateTime>("RefreshedAt");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasAnnotation("Npgsql:TableName", "dataset");
                });

            modelBuilder.Entity("NQuandl.Domain.Persistence.Entities.DatasetColumnName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ColumnIndex");

                    b.Property<string>("ColumnName");

                    b.Property<int>("DatasetId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("NQuandl.Domain.Persistence.Entities.RawResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Relational:ColumnType", "timestamp with time zone")
                        .HasAnnotation("Relational:GeneratedValueSql", "current_timestamp");

                    b.Property<string>("RequestUri");

                    b.Property<string>("ResponseContent")
                        .HasAnnotation("Relational:ColumnType", "jsonb");

                    b.HasKey("Id");

                    b.HasAnnotation("Npgsql:TableName", "raw_response");
                });

            modelBuilder.Entity("NQuandl.Domain.Persistence.Entities.Dataset", b =>
                {
                    b.HasOne("NQuandl.Domain.Persistence.Entities.Database")
                        .WithMany()
                        .HasForeignKey("DatabaseId");
                });

            modelBuilder.Entity("NQuandl.Domain.Persistence.Entities.DatasetColumnName", b =>
                {
                    b.HasOne("NQuandl.Domain.Persistence.Entities.Dataset")
                        .WithMany()
                        .HasForeignKey("DatasetId");
                });
        }
    }
}
