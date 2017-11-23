using System;
using Larch.Lib;
using Larch.Lib.Contracts;
using Newtonsoft.Json;

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