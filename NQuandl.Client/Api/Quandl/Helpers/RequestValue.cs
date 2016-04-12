using System;

namespace NQuandl.Client.Api.Quandl.Helpers
{
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
}