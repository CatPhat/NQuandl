﻿using NQuandl.Client.Requests;

namespace NQuandl.Client.Helpers
{
    public static class RequestParameterHelper
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


   


  
}