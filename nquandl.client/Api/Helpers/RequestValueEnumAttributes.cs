using System;

namespace NQuandl.Client.Api.Helpers
{
    internal static class GetRequestValueAttribute
    {
        internal static string GetStringValue(this Enum value)
        {
            string output = null;
            var type = value.GetType();
            var field = type.GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof (RequestValue), false) as RequestValue[];
            if (attributes != null && attributes.Length > 0)
            {
                output = attributes[0].Value;
            }
            return output;
        }
    }
}