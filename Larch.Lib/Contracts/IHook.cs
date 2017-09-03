using System;
using System.Collections.Generic;
using System.Text;

namespace Larch.Contracts
{
    public interface IHook
    {
        Level[] Levels();
        void Fire(Entry entry);
    }
}
