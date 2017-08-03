using System;
using Larch.Lib;
using Logger = Larch.Lib.Larch;

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
            var logger = new Logger();
            logger.WithFields(new Fields
            {
               {"name", "Amit"},
                {"age", 22 }
            }).Debug("Started writing Larch");
        }


    }
}