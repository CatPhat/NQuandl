using System;
using System.Collections.Generic;
using NQuandl.Domain.Persistence.Api.Entities;

namespace NQuandl.Domain.Persistence.Domain.Entities
{
    public class Dataset : EntityWithId<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string DatabaseCode { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RefreshedAt { get; set; }
        public string Frequency { get; set; }
        public virtual List<DatasetColumnName> ColumnNames { get; set; }
        public string Data { get; set; }

        public virtual Database Database { get; set; }
        public int DatabaseId { get; set; }

    }

    public class DatasetColumnName : EntityWithId<int>
    {
        public int DatasetId { get; set; }
        public virtual Dataset Dataset { get; set; }
        public int ColumnIndex { get; set; }
        public string ColumnName { get; set; }
    }
}