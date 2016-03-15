using System;
using System.Collections.Generic;
using NQuandl.Api;

namespace NQuandl.Domain.Responses
{
    public class DatabaseDataset : ResponseWithHttpMessage
    {
        public Dataset dataset { get; set; }
    }

    public class Dataset
    {
        public int id { get; set; }
        public string dataset_code { get; set; }
        public string database_code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime refreshed_at { get; set; }
        public string newest_available_date { get; set; }
        public string oldest_available_date { get; set; }
        public string[] column_names { get; set; }
        public string frequency { get; set; }
        public string type { get; set; }
        public bool premium { get; set; }
        public object limit { get; set; }
        public object transform { get; set; }
        public object column_index { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public object[][] data { get; set; }
        public object collapse { get; set; }
        public string order { get; set; }
        public int database_id { get; set; }
    }


    public class DatabaseDataset<TEntity> : DatabaseDataset where TEntity : QuandlEntity
    {
        public IEnumerable<TEntity> Entities { get; set; }
    }
}