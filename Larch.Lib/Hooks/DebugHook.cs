using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Larch.Lib.Contracts;

namespace Larch.Lib.Hooks
{
    public class DebugHook : IHook
    {
        public Level[] Levels()
        {
            return LevelExtensions.AllLevels();
        }

        public void Fire(Entry entry)
        {
            var log = entry.Logger.Formatter.Format(entry);
            Debug.WriteLine(log);
        }
    }
}
