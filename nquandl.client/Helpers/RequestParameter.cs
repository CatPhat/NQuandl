using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NQuandl.Client.Helpers;

namespace NQuandl.Client
{

    public static class RequestParameter
    {
        public static string SortOrder(SortOrder sortOrder)
        {
            return RequestParameterConstants.SortOrder + sortOrder.GetStringValue();
        }

        public static string ExcludeHeaders(Exclude excludeHeaders)
        {
            return RequestParameterConstants.ExcludeHeaders + excludeHeaders.GetStringValue();
        }

        public static string ExcludeData(Exclude excludeData)
        {
            return RequestParameterConstants.ExcludeData + excludeData.GetStringValue();
        }

        public static string Rows(int numberOfRows)
        {
            return RequestParameterConstants.Rows + numberOfRows;
        }

        public static string DateRange(DateRange dateRange)
        {
            const string dateFormat = "yyyy-mm-dd";

            return (RequestParameterConstants.TrimStart + dateRange.TrimStart.ToString(dateFormat)) + "&" +
                   (RequestParameterConstants.TrimEnd + dateRange.TrimEnd.ToString(dateFormat));
        }

        public static string Column(int columnNumber)
        {
            return RequestParameterConstants.Column + columnNumber;
        }

        public static string Transformation(Transformation transformation)
        {
            return RequestParameterConstants.Transformation + transformation.GetStringValue();
        }

        public static string ApiKey(string apiKey)
        {
            return RequestParameterConstants.AuthToken + apiKey;
        }
    }

    public class OptionalRequestParameters
    {
        public SortOrder? SortOrder { get; set; }
        public Exclude? ExcludeHeaders { get; set; }
        public int? Rows { get; set; }
        public DateRange DateRange { get; set; }
        public int? Column { get; set; }
        public Transformation? Transformation { get; set; }
        public Exclude? ExcludeData { get; set; }
        public string ApiKey { get; set; }
    }

    public class QuandlCode
    {
        public string TableCode { get; set; }
        public string DatabaseCode { get; set; }
    }
   

    public class DateRange
    {
        public DateTime TrimStart { get; set; }
        public DateTime TrimEnd { get; set; }
    }
    

   
}
