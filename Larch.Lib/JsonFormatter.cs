using System;
using System.Collections.Generic;
using System.Text;
using Larch.Lib.Contracts;
using Newtonsoft.Json;

namespace Larch.Lib
{
    public class JsonFormatter : IFormatter
    {
        public string Format(Entry entry)
        {
            var dic = new Fields(entry.Data);
            dic["msg"] = entry.Message;
            dic["time"] = entry.Timestamp.ToString("O");
            dic["level"] = entry.Level.ToStr();
            return JsonConvert.SerializeObject(dic);
        }
    }
}
