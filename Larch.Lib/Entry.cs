using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Larch.Lib
{
    public class Entry
    {
        public Larch Logger { get; set; }

        public Dictionary<string, object> Data { get; set; }

        public DateTime Timestamp { get; set; }

        public string Message { get; set; }

        public Level Level { get; set; }

        private byte[] buffer;

        public Entry(Larch logger) : this(logger, new Dictionary<string, object>())
        {
        }

        public Entry(Larch logger, Dictionary<string, object> data)
        {
            Logger = logger;
            Data = data;
        }

        public override string ToString()
        {
            return Logger.Formatter.Format(this);
        }

        public Entry WithException(Exception ex)
        {
            return WithField("Exception", ex);
        }

        private void Log(Level level, string msg)
        {
            Message = msg;
            Level = level;
            Timestamp = DateTime.Now;

            // TODO: hooks logic here
            // TODO: try catches
            // TODO: thread safety
            var formatted = Logger.Formatter.Format(this);
            Logger.Out.Write(Logger.Encoding.GetBytes(formatted));


        }

        public void Debug(params object[] objects) => Log(Level.DebugLevel, objects.ToFormattedString());

        public void Info(params object[] objects) => Log(Level.InfoLevel, objects.ToFormattedString());

        public void Warn(params object[] objects) => Log(Level.WarnLevel, objects.ToFormattedString());

        public void Error(params object[] objects) => Log(Level.ErrorLevel, objects.ToFormattedString());

        public void Fatal(params object[] objects)
        {
            var msg = objects.ToFormattedString();
            Log(Level.FatalLevel, msg);
            if (Data.TryGetValue("Exception", out object exObj))
            {
                var ex = exObj as Exception;
                if (ex != null)
                {
                    Environment.FailFast(msg, ex);
                }
            }
            Environment.FailFast(msg);
        }

        public Entry WithField(string key, object value)
        {
            var newData = new Dictionary<string, object>(Data) { { key, value } };
            return new Entry(Logger, newData);
        }

        public Entry WithFields(Dictionary<string, object> fields)
        {
            var newData = new Dictionary<string, object>(fields.Count + Data.Count);
            foreach (var keyvalpair in fields.Concat(Data))
            {
                newData.Add(keyvalpair.Key, keyvalpair.Value);
            }
            return new Entry(Logger, newData);
        }
    }
}
