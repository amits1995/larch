using System;
using System.Collections.Generic;
using System.Text;

namespace Larch.Lib.Contracts
{
    public interface IFormatter
    {
        string Format(Entry entry);
    }
}
