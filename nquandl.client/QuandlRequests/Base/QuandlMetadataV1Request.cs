using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client.Models.QuandlRequests
{
    public class QuandlMetadataV1Request : BaseQuandlRequestV1<QuandlMetadataV1Response>
    {
        public QuandlMetadataV1Request(string databaseCode, string tableCode)
        {
            if (String.IsNullOrEmpty(databaseCode)) throw new NullReferenceException("databaseCode");
            if (String.IsNullOrEmpty(tableCode)) throw new NullReferenceException("tableCode");

            _databaseCode = databaseCode.ToUpper();
            _tableCode = tableCode.ToUpper();
        }

        private readonly string _databaseCode;
        private readonly string _tableCode;

        public override string QueryCode
        {
            get { return _databaseCode + "/" + _tableCode; }
        }

        public override string Parameters
        {
            get { return "exclude_data=true"; }
        }
    }

    [DataContract]
    public class QuandlMetadataV1Response : QuandlResponse
    {
        [DataMember(Name = "errors")]
        public Dictionary<string, string> Errors { get; internal set; }

        [DataMember(Name = "id")]
        public int MetadataId { get; internal set; }

        [DataMember(Name = "source_name")]
        public string SourceName { get; internal set; }

        [DataMember(Name = "source_code")]
        public string SourceCode { get; internal set; }

        [DataMember(Name = "code")]
        public string Code { get; internal set; }

        [DataMember(Name = "name")]
        public string Name { get; internal set; }

        [DataMember(Name = "urlize_name")]
        public string UrlizeName { get; internal set; }

        [DataMember(Name = "display_url")]
        public string DisplayUrl { get; internal set; }

        [DataMember(Name = "description")]
        public string Descrption { get; internal set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; internal set; }

        [DataMember(Name = "frequency")]
        public string Frequency { get; internal set; }

        [DataMember(Name = "from_date")]
        public string FromDate { get; internal set; }

        [DataMember(Name = "to_date")]
        public string ToDate { get; internal set; }

        [DataMember(Name = "column_names")]
        public string[] ColumnNames { get; internal set; }

        [DataMember(Name = "_private")]
        public bool Private { get; internal set; }

        [DataMember(Name = "type")]
        public object Type { get; internal set; }

        [DataMember(Name = "premium")]
        public bool Premium { get; internal set; }
    }
}
