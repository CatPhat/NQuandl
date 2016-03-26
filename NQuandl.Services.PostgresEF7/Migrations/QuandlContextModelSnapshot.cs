using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using NQuandl.Services.PostgresEF7.Models;

namespace NQuandl.Services.PostgresEF7.Migrations
{
    [DbContext(typeof(QuandlContext))]
    partial class QuandlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("NQuandl.Services.PostgresEF7.Models.QuandlDatabase", b =>
                {
                    b.Property<int>("DatabaseId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("DatabaseId");
                });

            modelBuilder.Entity("NQuandl.Services.PostgresEF7.Models.QuandlDataset", b =>
                {
                    b.Property<int>("DatasetId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int?>("QuandlDatabaseDatabaseId");

                    b.HasKey("DatasetId");
                });

            modelBuilder.Entity("NQuandl.Services.PostgresEF7.Models.QuandlDataset", b =>
                {
                    b.HasOne("NQuandl.Services.PostgresEF7.Models.QuandlDatabase")
                        .WithMany()
                        .HasForeignKey("QuandlDatabaseDatabaseId");
                });
        }
    }
}
