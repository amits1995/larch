using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

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

        
    }
}
