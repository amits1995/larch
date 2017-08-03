using System;
using System.Collections.Generic;
using System.Text;

namespace Larch.Lib.Contracts
{
    public interface IOutputAdapter
    {
        void Write(string log);
    }
}
