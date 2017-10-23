using System;
using System.Collections.Generic;
using System.Text;

namespace Larch.Lib.Attributes
{
    public interface ICustomRenderer
    {
        string DoRender(object obj);
    }
    public class LarchRender : Attribute
    {
        public ICustomRenderer Renderer { get; }

        public LarchRender(ICustomRenderer renderer)
        {
            Renderer = renderer;
        }
    }
}
