using System;
using Larch.Lib;
using Larch.Lib.Hooks;
using Microsoft.Extensions.Logging;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            Example1();
        }

        private static void Example1()
        {
            var logger = Logger.DefaultLogger();
            logger.Hooks.Add(new DebugHook());
            var e = logger.WithFields(new Fields
            {
                {"name", "Amit"},
                {"age", 22}
            });
            e.Debug("Started writing Logger");
            e.Info("reusing the same entry");
        }
    }
}