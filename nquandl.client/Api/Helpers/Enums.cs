namespace NQuandl.Client.Api.Helpers
{
    public enum SortOrder
    {
        [RequestValue("asc")] Ascending,

        [RequestValue("desc")] Descending,
    }

    public enum Exclude
    {
        [RequestValue("true")] True,

        [RequestValue("false")] False
    }

    public enum Frequency
    {
        [RequestValue("none")] None,

        [RequestValue("daily")] Daily,

        [RequestValue("weekly")] Weekly,

        [RequestValue("monthly")] Monthly,

        [RequestValue("quarterly")] Quarterly,

        [RequestValue("annual")] Annual,
    }

    public enum Transformation
    {
        [RequestValue("diff")] RowOnRowChange,

        [RequestValue("rdiff")] RowOnRowPercentageChange,

        [RequestValue("cumul")] CumulativeSum,

        [RequestValue("normalize")] Normalize,
    }

    public enum ResponseFormat
    {
        [RequestValue("json")] JSON,

        [RequestValue("xml")] XML,

        [RequestValue("csv")] CSV,
    }
}