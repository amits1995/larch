using System;
using Larch.Lib.Contracts;

namespace Larch.Lib.Hooks
{
    public class ConsoleHook : IHook
    {
        private readonly Level[] _levels;
        private volatile object _lock = new object();

        public ConsoleHook(Level[] levels)
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
            lock (_lock)
            {
                Console.ForegroundColor = GetColor(entry);
                Console.WriteLine(log);
                Console.ResetColor();
            }
        }

        private ConsoleColor GetColor(Entry entry)
        {
            switch (entry.Level)
            {
                case Level.FatalLevel:
                case Level.ErrorLevel:
                    return ConsoleColor.Red;
                case Level.WarnLevel:
                    return ConsoleColor.Yellow;
                case Level.InfoLevel:
                    return ConsoleColor.DarkGreen;
                case Level.DebugLevel:
                    return ConsoleColor.DarkGray;
                default:
                    return ConsoleColor.White;
            }
        }
    }
}
