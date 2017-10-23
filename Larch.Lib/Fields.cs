using System;
using System.Collections.Generic;
using System.Text;

namespace Larch.Lib
{
    public class Fields : Dictionary<string, object>
    {
        public Fields()
        {
        }

        public Fields(Fields fields) : base(fields)
        {

        }

        public Fields(int capacity) : base(capacity)
        {
        }
    }
}
