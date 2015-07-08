using System;

namespace NQuandl.Client.Domain.Responses
{
    public class JsonResponseV2 : JsonResponse
    {
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
        public object display_url { get; set; }
        public string description { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime refreshed_at { get; set; }
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