using System;
using System.Collections.Generic;
using NQuandl.Api.Persistence;
using NQuandl.Api.Persistence.Entities;

namespace NQuandl.Domain.Persistence.Entities
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

        //public string[] ColumnNames { get; set; }
        public string Data { get; set; }
    }
}