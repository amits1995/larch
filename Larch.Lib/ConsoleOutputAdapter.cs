using System;
using System.Collections.Generic;
using System.Text;
using Larch.Lib.Contracts;

namespace Larch.Lib
{
    public class ConsoleOutputAdapter : IHook
    {
        private readonly Level[] _levels;
        private volatile object _lock = new object();
        public ConsoleColor DebugColor { get; set; } = ConsoleColor.Cyan;
        public ConsoleColor ErrorColor { get; set; } = ConsoleColor.Red;
        public ConsoleColor WarningColor { get; set; } = ConsoleColor.Yellow;
        public ConsoleColor InfoColor { get; set; } = ConsoleColor.DarkGreen;

        public ConsoleOutputAdapter(Level[] levels)
        {
            _levels = levels;
        }

        public Level[] Levels()
        {
            return _levels;
        }

        public void Fire(Entry entry)
        {
            var log = entry.Logger.Formatter.Format(entry);
            var (startIndex, length) = LevelIndices(entry, log);
            lock (_lock)
            {
                if (length != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(log.Substring(0, startIndex));
                    Console.ForegroundColor = GetColor(entry);
                    Console.Write(log.Substring(startIndex, length));
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(log.Substring(startIndex + length));
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(log);
                    Console.ResetColor();
                }
            }
        }

        private (int, int) LevelIndices(Entry entry, string entryFormatted)
        {
            var levelStr = entry.Level.ToStr().Substring(0, 4);
            var startIndex = entryFormatted.IndexOf(levelStr, StringComparison.OrdinalIgnoreCase);
            if (startIndex != -1)
            {
                return (startIndex, levelStr.Length);
            }
            return (0, 0);
        }

        private ConsoleColor GetColor(Entry entry)
        {
            switch (entry.Level)
            {
                case Level.FatalLevel:
                case Level.ErrorLevel:
                    return ErrorColor;
                case Level.WarnLevel:
                    return WarningColor;
                case Level.InfoLevel:
                    return InfoColor;
                case Level.DebugLevel:
                    return DebugColor;
                default:
                    return ConsoleColor.White;
            }
        }
    }
}
