namespace NQuandl.Client.Helpers
{
    public static class RequestParameterConstants
    {
        //v1
        public const string SortOrder = "sort_order";
        public const string ExcludeHeaders = "exclude_headers";
        public const string Rows = "rows";
        public const string TrimStart = "trim_start";
        public const string TrimEnd = "trim_end";
        public const string Column = "column";
        public const string Frequency = "collapse";
        public const string Transformation = "transformation";
        public const string ExcludeData = "exclude_data";
        public const string ApiVersion1 = "v1/datasets";

        //v1 & v2
        public const string AuthToken = "auth_token";

        //v2
        public const string ApiVersion2 = "v2/datasets";
        public const string Query = "query";
        public const string SourceCode = "source_code";
        public const string PerPage = "per_page";
        public const string Page = "page";
    }
}