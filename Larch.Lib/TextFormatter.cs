using Larch.Lib.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Larch.Lib
{
    public class TextFormatter : IFormatter
    {
        private const string DefautDatetimeFormat = "o"; // ISO 8601
        private const int MessagePad = 40;
        public bool DisableSorting { get; set; }
        public bool QuoteEmptyStrings { get; set; }
        public bool DisableTimestamp { get; set; }
        public string TimestampFormat { get; set; }
        public bool FullTimestamp { get; set; }
        public DateTime BaseDateTime { get; set; } = DateTime.UtcNow;

        public string Format(Entry entry)
        {
            var sBuilder = new StringBuilder();
            IEnumerable<string> keys;
            if (DisableSorting)
            {
                keys = entry.Data.Keys.ToList();
            }
            else
            {
                keys = new SortedSet<string>(entry.Data.Keys.ToList());
            }

            if (DisableTimestamp)
            {
                sBuilder.Append($"{entry.Level.ToStr().Substring(0, 4).ToUpper()} {entry.Message.PadRight(MessagePad)} ");
            }
            else if (!FullTimestamp)
            {
                var secondsPassed = (int)entry.Timestamp.Subtract(BaseDateTime).TotalSeconds;
                sBuilder.Append($"{entry.Level.ToStr().Substring(0, 4).ToUpper()}[{secondsPassed:D4}] {entry.Message.PadRight(MessagePad)} ");
            }
            else
            {
                sBuilder.Append($"{entry.Level.ToStr().Substring(0, 4).ToUpper()}[{entry.Timestamp.ToString(string.IsNullOrEmpty(TimestampFormat) ? DefautDatetimeFormat : TimestampFormat)}] {entry.Message.PadRight(MessagePad)} ");
            }

            //AppendKeyValue(sBuilder, "level", entry.Level.ToStr());
            //if (!DisableTimestamp)
            //{
            //    AppendKeyValue(sBuilder, "time", entry.Timestamp.ToString(string.IsNullOrEmpty(TimestampFormat) ? DefautDatetimeFormat : TimestampFormat));
            //}

            foreach (var key in keys)
            {
                AppendKeyValue(sBuilder, key, entry.Data[key]);
            }
            //sBuilder.AppendLine();
            return sBuilder.ToString();
        }

        private void AppendKeyValue(StringBuilder sBuilder, string key, object value)
        {
            if (sBuilder.Length > 0)
            {
                sBuilder.Append(" ");
            }
            sBuilder.Append($"{key}=");
            AppendValue(sBuilder, value);
        }

        private void AppendValue(StringBuilder sBuilder, object value)
        {
            var str = value.Render();
            if (NeedsQouting(str))
                sBuilder.Append($"\"{str}\"");
            else
                sBuilder.Append(str);
        }

        private bool NeedsQouting(string text)
        {
            if (QuoteEmptyStrings && string.IsNullOrEmpty(text))
            {
                return true;
            }

            foreach (var c in text) // todo: shouldn't this be more efficient?
            {
                if (!((c >= 'a' && c <= 'z') ||
            (c >= 'A' && c <= 'Z') ||
            (c >= '0' && c <= '9') ||
            c == '-' || c == '.' || c == '_' || c == '/' || c == '@' || c == '^' || c == '+'))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
