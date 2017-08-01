using Larch.Lib.Contracts;
using System;
using System.Collections.Concurrent;
using System.Text;

namespace Larch.Lib
{
    public class Larch
    {
        public ConcurrentBag<IHook> Hooks { get; set; }

        public IOutputAdapter Out { get; set; }

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        private IFormatter _formatter = new TextFormatter();

        public IFormatter Formatter
        {
            get
            {
                lock (_lock)
                {
                    return _formatter;
                }
            }
            set
            {
                lock (_lock)
                {
                    _formatter = value;
                }
            }
        }

        private volatile object _lock = new object();
    }
}
