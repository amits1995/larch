using System;
using System.Collections.Generic;
using System.Text;
using Larch.Lib.Contracts;

namespace Larch.Lib
{
    public class ConsoleOutputAdapter : IOutputAdapter
    {
        public void Write(string log)
        {
            Console.WriteLine(log);
        }
    }
}
