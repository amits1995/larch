using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Larch
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

        public static Fields Destruct(this object obj)
        {
            var fields = new Fields();
            foreach (var prop in obj.GetType().GetRuntimeProperties())
            {
                if (prop.GetCustomAttribute<Attributes.LarchIgnore>() != null) continue;
                var lProp = prop.GetCustomAttribute<Attributes.LarchProperty>();
                fields[lProp != null ? lProp.PropertyKey : prop.Name] = prop.GetValue(obj);
            }
            return fields;
        }



        
    }
}
