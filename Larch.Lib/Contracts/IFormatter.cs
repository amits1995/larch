using System;
using System.Collections.Generic;
using System.Text;

namespace Larch.Contracts
{
    public interface IFormatter
    {
        string Format(Entry entry);
    }
}
