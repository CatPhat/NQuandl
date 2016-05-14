namespace NQuandl.Client.Api.Quandl.Helpers
{
    public enum Order
    {
        [RequestValue("asc")] Ascending,

        [RequestValue("desc")] Descending
    }

   
    public enum Collapse
    {
        [RequestValue("none")] None,
        
        [RequestValue("daily")] Daily,

        [RequestValue("weekly")] Weekly,
        
        [RequestValue("monthly")] Monthly,

        [RequestValue("quarterly")] Quarterly,

        [RequestValue("annual")] Annual

    }

    public enum Transform
    {
        [RequestValue("none")] None,

        [RequestValue("diff")] RowOnRowChange,

        [RequestValue("rdiff")] RowOnRowPercentageChange,

        [RequestValue("cumul")] CumulativeSum,

        [RequestValue("normalize")] Normalize
    }

    public enum ResponseFormat
    {
        [RequestValue("json")] JSON,

        [RequestValue("xml")] XML,

        [RequestValue("csv")] CSV
    }
}