using System;
using System.Runtime.CompilerServices;
using NQuandl.Client.Api;
using NQuandl.Client.Api.Helpers;

namespace NQuandl.Client.Domain.RequestParameters
{
    public class RequiredDataRequestParameters : QuandlRequestParameters
    {
        // required
        public string DatabaseCode { get; set; }
        public string DatasetCode { get; set; }

        public OptionalDataRequestParameters OptionalParameters { get; set; }
        
    }

    public class OptionalDataRequestParameters : QuandlRequestParameters
    {
        // optional
        public int? Limit { get; set; }
        public int? Rows { get; set; }
        public int? ColumnIndex { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Order? Order { get; set; }
        public Collapse? Collapse { get; set; }
        public Transform? Transform { get; set; }
    }

    public class DataRequestParameters<TEntity> where TEntity : QuandlEntity 
    {
        public OptionalDataRequestParameters OptionalParameters { get; set; }
    }
}