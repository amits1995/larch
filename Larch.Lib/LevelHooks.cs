using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Larch.Lib.Contracts;

namespace Larch.Lib
{
    public class LevelHooks
    {
        private readonly Dictionary<Level, ConcurrentBag<IHook>> _levelHooks;

        public LevelHooks()
        {
            _levelHooks = new Dictionary<Level, ConcurrentBag<IHook>>();
            foreach (var level in LevelExtensions.AllLevels())
            {
                _levelHooks[level] = new ConcurrentBag<IHook>();
            }
        }

        public void Add(IHook hook)
        {
            foreach (var level in hook.Levels())
            {
                _levelHooks[level].Add(hook);
            }
        }

        public void Fire(Entry entry)
        {
            foreach (var hook in _levelHooks[entry.Level])
            {
                try
                {
                    hook.Fire(entry);
                }
                catch (Exception e)
                {
                    var ht = hook.GetType();
                    System.Diagnostics.Debug.WriteLine($"Hook {ht.Name} threw an exception: {e.Message}");
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                }
            }
        }
    }
}
