using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client
{
    public enum SortOrder
    {
        [RequestValue("asc")]
        Ascending,

        [RequestValue("desc")]
        Descending,
    }

    public enum ExcludeHeaders
    {
        [RequestValue("true")]
        True,

        [RequestValue("false")]
        False
    }

    public enum Frequency
    {
        [RequestValue("none")]
        None,

        [RequestValue("daily")]
        Daily,

        [RequestValue("weekly")]
        Weekly,

        [RequestValue("monthly")]
        Monthly,

        [RequestValue("quarterly")]
        Quarterly,

        [RequestValue("annual")]
        Annual,
    }

    public enum Transformation
    {
        [RequestValue("diff")]
        RowOnRowChange,

        [RequestValue("rdiff")]
        RowOnRowPercentageChange,

        [RequestValue("cumul")]
        CumulativeSum,

        [RequestValue("normalize")]
        Normalize,
    }
}
