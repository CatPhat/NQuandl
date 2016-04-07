using System.Collections.Generic;
using NQuandl.Domain.Persistence.Api.Entities;

namespace NQuandl.Domain.Persistence.Domain.Entities
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