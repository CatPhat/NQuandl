using System.Collections.Generic;
using NQuandl.PostgresEF7.Api.Entities;

namespace NQuandl.PostgresEF7.Domain.Entities
{
    public class Database : EntityWithId<int>
    {
        public string Name { get; set; }
        public string DatabaseCode { get; set; }
        public string Description { get; set; }
        public int DatasetsCount { get; set; }
        public long Downloads { get; set; }
        public bool Premium { get; set; }
        public string Image { get; set; }

        public virtual List<Dataset> Datasets { get; set; }
    }
}