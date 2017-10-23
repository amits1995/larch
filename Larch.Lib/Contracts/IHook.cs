using System;
using System.Collections.Generic;
using System.Text;

namespace Larch.Lib.Contracts
{
    public interface IHook
    {
        Level[] Levels();
        void Fire(Entry entry);
    }
}
