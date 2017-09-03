using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Larch
{
    public enum Level
    {
        FatalLevel,
        ErrorLevel,
        WarnLevel,
        InfoLevel,
        DebugLevel
    }
    public static class LevelExtensions
    {
        public static Level[] GetLevels(Level minLevel) =>
            AllLevels().Where(level => level <= minLevel).ToArray();

        public static Level[] AllLevels()
        {
           return new[]
           {
               Level.FatalLevel,
               Level.ErrorLevel,
               Level.WarnLevel,
               Level.InfoLevel,
               Level.DebugLevel,
           }; 
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
