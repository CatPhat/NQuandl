namespace NQuandl.Domain.RequestParameters
{
    public class RequestParameter
    {
        private readonly string _name;
        private readonly string _value;

        internal RequestParameter(string name, string value)
        {
            _name = name;
            _value = value;
        }

        internal string Name
        {
            get { return _name; }
        }

        internal string Value
        {
            get { return _value; }
        }
    }
}