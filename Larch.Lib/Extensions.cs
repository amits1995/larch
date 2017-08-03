using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Larch.Lib
{
    public class Fields : Dictionary<string, object>
    {
        public Fields()
        {
        }

        public Fields(Fields fields) : base(fields)
        {
            
        }

        public Fields(int capacity) : base(capacity)
        {
        }
    }
    public static class Extensions
    {
        public static string ToFormattedString(this object[] objects)
        {
            var stringBuilder = new StringBuilder();
            foreach (var obj in objects)
            {
                stringBuilder.Append(obj.ToString());
            }
            return stringBuilder.ToString();
        }

        public static string ToStr(this Level level)
        {
            return LogLevelToString(level);
        }

        public static string LogLevelToString(Level level)
        {
            switch (level)
            {
                case Level.DebugLevel:
                    return "Debug";
                case Level.ErrorLevel:
                    return "Error";
                case Level.FatalLevel:
                    return "Fatal";
                case Level.InfoLevel:
                    return "Info";
                case Level.WarnLevel:
                    return "Warn";
            }
            return string.Empty;
        }
    }
}
