using System;
using System.Collections.Generic;
using System.Text;

namespace Larch.Attributes
{
    public class LarchProperty : Attribute
    {
        public string PropertyKey { get; set; }
        public LarchProperty(string propertyKey)
        {
            PropertyKey = propertyKey;
        }
    }
}
