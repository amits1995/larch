using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Larch.Lib
{
    public static class Extensions
    {
        public static string ToFormattedString(this object[] objects)
        {
            var stringBuilder = new StringBuilder();
            foreach (var obj in objects)
            {
                stringBuilder.Append(obj);
            }
            return stringBuilder.ToString();
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
