using System;
using Larch;
using Larch.Hooks;
using Microsoft.Extensions.Logging;

namespace Examples
{
    class Program
    {
        [Larch.Attributes.LarchProperty("proop")]
        public int MyProperty { get; set; }
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