using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Larch.Lib.Attributes;

namespace Larch.Lib
{

    public static class Extensions
    {
        public static string ToFormattedString(this object[] objects)
        {
            var stringBuilder = new StringBuilder();
            foreach (var obj in objects)
            {
                // ReSharper disable once RedundantToStringCall
                stringBuilder.Append(obj.ToString());
            }
            return stringBuilder.ToString();
        }

        public static string Render(this object obj)
        {
            switch (obj)
            {
                case byte[] buf:
                    return Convert.ToBase64String(buf);
                default:
                    return obj.ToString();
            }
        }

        public static Fields Destruct(this object obj)
        {
            var fields = new Fields();
            foreach (var prop in obj.GetType().GetRuntimeProperties())
            {
                if (prop.GetCustomAttribute<LarchIgnore>() != null) continue;
                var customRenderer = prop.GetCustomAttribute<LarchRender>();
                fields.Add(prop.Name, customRenderer == null ? prop.GetValue(obj) : customRenderer.Renderer.DoRender(obj));
            }
            return fields;
        }
    }
}
