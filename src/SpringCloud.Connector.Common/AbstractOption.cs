using System.Text;

namespace SpringCloud.Connector.Common
{
    public class AbstractOption
    {
        protected char KeyValueTerm;
        protected char KeyValueSep;

        protected const char DefaultTerminator = ';';
        protected const char DefaultSeparator = '=';

        protected AbstractOption() :
            this(DefaultTerminator, DefaultSeparator)
        {
        }

        protected AbstractOption(char keyValueTerm, char keyValueSep)
        {
            KeyValueSep = keyValueSep;
            KeyValueTerm = keyValueTerm;
        }

        protected void AddKeyValue(StringBuilder sb, string key, int value)
        {
            AddKeyValue(sb, key, value.ToString());
        }

        protected void AddKeyValue(StringBuilder sb, string key, bool value)
        {
            AddKeyValue(sb, key, value.ToString().ToLowerInvariant());
        }

        protected void AddKeyValue(StringBuilder sb, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append(key);
                sb.Append(KeyValueSep);
                sb.Append(value);
                sb.Append(KeyValueTerm);
            }
        }
    }
}