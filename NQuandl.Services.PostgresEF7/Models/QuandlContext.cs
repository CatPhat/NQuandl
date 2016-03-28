using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.Data.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NQuandl.Services.PostgresEF7.Models
{
    public class QuandlContext : DbContext
    {
        public DbSet<QuandlDatabase> Databases { get; set; }
        public DbSet<QuandlDataset> Datasets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuandlDatabase>().HasKey(x => x.Id);
            modelBuilder.Entity<QuandlDataset>().HasKey(x => x.Id);
           
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=192.168.1.2;Username=postgres;Password=postgres;Database=nquandl");
        }
    }

    public class QuandlDatabase
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("datasets")]
        public virtual List<QuandlDataset> Datasets { get; set; } 
    }


    public class QuandlDataset
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("dataset_code")]
        public string DatasetCode { get; set; }

        [JsonProperty("database_code")]
        public string DatabaseCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("refreshed_at")]
        public DateTime RefreshedAt { get; set; }

        [JsonProperty("newest_available_date")]
        public string NewestAvaialbleDate { get; set; }

        [JsonProperty("oldest_available_date")]
        public string OldestAvailableDate { get; set; }

        [JsonProperty("column_names")]
        public string[] ColumnNames { get; set; }

        [JsonProperty("frequency")]
        public string Frequency { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("premium")]
        public bool Premium { get; set; }

        [JsonProperty("limit")]
        public object Limit { get; set; }

        [JsonProperty("transform")]
        public object Transform { get; set; }

        [JsonProperty("column_index")]
        public object ColumnIndex { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("end_date")]
        public string EndDate { get; set; }

        [JsonProperty("data")]
        public List<List<object>> Data { get; set; }

        [JsonProperty("collapse")]
        public object Collapse { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }

        [JsonProperty("database_id")]
        public int DatabaseId { get; set; }
    }
}