using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NQuandl.Client.Api;

namespace NQuandl.Client.Domain.Responses
{
    [DataContract]
    public class JsonResponseV1 : JsonResponse
    {
        [DataMember(Name = "errors")]
        public Dictionary<string, string> Errors { get; set; }

        [DataMember(Name = "id")]
        public int MetadataId { get; set; }

        [DataMember(Name = "source_name")]
        public string SourceName { get; set; }

        [DataMember(Name = "source_code")]
        public string SourceCode { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "urlize_name")]
        public string UrlizeName { get; set; }

        [DataMember(Name = "display_url")]
        public string DisplayUrl { get; set; }

        [DataMember(Name = "description")]
        public string Descrption { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "frequency")]
        public string Frequency { get; set; }

        [DataMember(Name = "from_date")]
        public string FromDate { get; set; }

        [DataMember(Name = "to_date")]
        public string ToDate { get; set; }

        [DataMember(Name = "column_names")]
        public string[] ColumnNames { get; set; }

        [DataMember(Name = "_private")]
        public bool Private { get; set; }

        [DataMember(Name = "type")]
        public object Type { get; set; }

        [DataMember(Name = "premium")]
        public bool Premium { get; set; }

        [DataMember(Name = "data")]
        public object[][] Data { get; set; }
    }

    public class JsonResponseV1<TEntity> : JsonResponseV1 where TEntity : QuandlEntity
    {
        public IEnumerable<TEntity> Entities { get; set; }
    }
}