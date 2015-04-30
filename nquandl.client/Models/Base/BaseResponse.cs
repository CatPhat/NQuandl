using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client.Models
{
    public interface IQuandlResponse<TResponse> where TResponse : QuandlResponse
    {
        QuandlCode QuandlCode { get; }
    }

    public abstract class QuandlResponse
    {
        private readonly QuandlCode _quandlCode;
        protected QuandlResponse(QuandlCode quandlCode)
        {
            _quandlCode = quandlCode;
        }

        
        public QuandlCode QuandlCode
        {
            get { return _quandlCode; }
        }
    }

    [DataContract]
    public class QuandlV1Response : QuandlResponse
    {
        public QuandlV1Response(QuandlCode quandlCode) : base(quandlCode)
        {
        }

        [DataMember(Name = "errors")]
        public Dictionary<string, string> Errors { get;  set; }

        [DataMember(Name = "id")]
        public int MetadataId { get;  set; }

        [DataMember(Name = "source_name")]
        public string SourceName { get;  set; }

        [DataMember(Name = "source_code")]
        public string SourceCode { get;  set; }

        [DataMember(Name = "code")]
        public string Code { get;  set; }

        [DataMember(Name = "name")]
        public string Name { get;  set; }

        [DataMember(Name = "urlize_name")]
        public string UrlizeName { get;  set; }

        [DataMember(Name = "display_url")]
        public string DisplayUrl { get;  set; }

        [DataMember(Name = "description")]
        public string Descrption { get;  set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get;  set; }

        [DataMember(Name = "frequency")]
        public string Frequency { get;  set; }

        [DataMember(Name = "from_date")]
        public string FromDate { get;  set; }

        [DataMember(Name = "to_date")]
        public string ToDate { get;  set; }

        [DataMember(Name = "column_names")]
        public string[] ColumnNames { get;  set; }

        [DataMember(Name = "_private")]
        public bool Private { get;  set; }

        [DataMember(Name = "type")]
        public object Type { get;  set; }

        [DataMember(Name = "premium")]
        public bool Premium { get;  set; }

        [DataMember(Name = "data")]
        public object[][] Data { get; set; }
    }


    [Serializable]
    public class ResponseWithV2Metadata : QuandlResponse
    {
        public ResponseWithV2Metadata(QuandlCode quandlCode) : base(quandlCode)
        {
        }

        public int total_count { get; set; }
        public int current_page { get; set; }
        public int per_page { get; set; }
        public Doc[] docs { get; set; }
        public Source[] sources { get; set; }
        public int start { get; set; }
        public object spellcheck { get; set; }
        public object[][] highlighting { get; set; }


    }

    public class Doc
    {
        public int id { get; set; }
        public int source_id { get; set; }
        public string source_code { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string urlize_name { get; set; }
        public string display_url { get; set; }
        public string description { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? refreshed_at { get; set; }
        public string frequency { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string[] column_names { get; set; }
        public bool _private { get; set; }
        public object type { get; set; }
        public bool premium { get; set; }
    }


    public class Source
    {
        public int id { get; set; }
        public string code { get; set; }
        public int datasets_count { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public string host { get; set; }
        public int concurrency { get; set; }
        public bool use_proxy { get; set; }
        public bool premium { get; set; }
    }
}
