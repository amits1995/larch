using Larch.Lib.Contracts;
using System;
using System.IO;
using Larch.Lib;
using Newtonsoft.Json;

namespace JsonFormatter
{
    public class JsonFormatter : IFormatter
    {
        public string TimestampFormat { get; set; }
        public string DefaultDatetimeFormat { get; set; } = "o"; // ISO 8601
        private readonly JsonSerializer _serializer;

        public JsonFormatter() : this(new JsonSerializer())
        {

        }
        public JsonFormatter(JsonSerializer serializer)
        {
            _serializer = serializer;
        }

        public string Format(Entry entry)
        {
            var fields = new Fields(entry.Data)
            {
                {
                    "time",
                    entry.Timestamp.ToString(
                        string.IsNullOrEmpty(TimestampFormat) ? DefaultDatetimeFormat : TimestampFormat)
                },
                {"level", entry.Level.ToStr()},
                {"msg", entry.Message}
            };
            string json;
            using (var writer = new StringWriter())
            {
                _serializer.Serialize(writer, fields);
                json = writer.ToString();
            }
            return json;
        }
    }
}
