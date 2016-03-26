using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.Data.Entity;

namespace NQuandl.Services.PostgresEF7.Models
{
    public class QuandlContext : DbContext
    {
        public DbSet<QuandlDatabase> Databases { get; set; }
        public DbSet<QuandlDataset> Datasets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuandlDatabase>().HasKey(x => x.DatabaseId);
            modelBuilder.Entity<QuandlDataset>().HasKey(x => x.DatasetId);
           
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=192.168.1.2;Username=postgres;Password=postgres;Database=nquandl");
        }
    }

    public class QuandlDatabase
    {
        public int DatabaseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<QuandlDataset> Datasets { get; set; } 
    }

    public class QuandlDataset
    {
        public int DatasetId { get; set; }
       public string Name { get; set; }
        public string Description { get; set; }
    }
}