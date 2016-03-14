using System;
using NQuandl.Api.Helpers;

namespace NQuandl.Domain.RequestParameters
{
    public class DataRequestParameters
    {
        // required
        public string DatabaseCode { get; set; }
        public string DatasetCode { get; set; }

        // optional
        public int? Limit { get; set; }
        public int? Rows { get; set; }
        public int? ColumnIndex { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Order? Order { get; set; }
        public Collapse? Collapse { get; set; }
        public Transform? Transform { get; set; }
        public string ApiKey { get; set; }
    }

   
}
