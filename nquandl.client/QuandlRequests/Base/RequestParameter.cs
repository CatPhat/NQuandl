using System;

namespace NQuandl.Client
{
    public class RequestParameter
    {
        public string SortOrder(SortOrder sortOrder)
        {
            return RequestParameterConstants.SortOrder + sortOrder.GetStringValue();
        }

        public string ExcludeHeaders(ExcludeHeaders excludeHeaders)
        {
            return RequestParameterConstants.ExcludeHeaders + excludeHeaders.GetStringValue();
        }

        public string Rows(int numberOfRows)
        {
            return RequestParameterConstants.Rows + numberOfRows;
        }

        public string DateRange(DateTime trimStart, DateTime trimEnd)
        {
            const string dateFormat = "yyyy-mm-dd";
         
            return (RequestParameterConstants.TrimStart + trimStart.ToString(dateFormat)) + "&" +
                   (RequestParameterConstants.TrimEnd + trimEnd.ToString(dateFormat));
        }

        public string Column(int columnNumber)
        {
            return RequestParameterConstants.Column + columnNumber;
        }

        public string Transformation(Transformation transformation)
        {
            return RequestParameterConstants.Transformation + transformation.GetStringValue();
        }
    }

    internal class RequestValue : Attribute
    {
        private readonly string _value;
        public RequestValue(string value)
        {
            _value = value;
        }
        public string Value
        {
            get { return _value; }
        }
    }

    internal static class GetRequestValueAttribute
    {
        internal static string GetStringValue(this Enum value)
        {
            string output = null;
            var type = value.GetType();
            var field = type.GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(RequestValue), false) as RequestValue[];
            if (attributes != null && attributes.Length > 0)
            {
                output = attributes[0].Value;
            }
            return output;
        }
    }
}
