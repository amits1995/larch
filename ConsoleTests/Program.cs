using System;
using Larch.Lib;
using Larch.Lib.Contracts;
using Newtonsoft.Json;

namespace Examples
{
    public class Tree
    {
        public String Name { get; set; }

        public int LocationX { get; set; }
        public int LocationY { get; set; }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Example1();
            Example2();
        }

        private static void Example2()
        {
            var logger = Logger.DefaultLogger();

            var tree = new Tree { LocationX = 1, LocationY = 2, Name = "Pickle Rick" };

            logger.WithFields(tree.Destruct()).Info("Destructing an object to fields");
        }

        private static void Example1()
        {
            var logger = Logger.DefaultLogger();

            var e = logger.WithFields(new Fields
            {
                {"name", "Amit"},
                {"age", 22},
                {"guid",Guid.NewGuid().ToByteArray() }
            });
            e.Debug("Started writing Logger");
            e = e.WithFields(new Fields
            {
                {"filename", "file.bin"}
            });
            e = e.WithException(new Exception("blah"));
            e.Error("something went wrong while handlnig file");
            e.Info("reusing the same entry");
        }
    }
}