using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Larch
{
    public class Entry
    {
        public Logger Logger { get; set; }

        public Fields Data { get; set; }

        public DateTime Timestamp { get; set; }

        public string Message { get; set; }

        public Level Level { get; set; }

        public Entry(Logger logger) : this(logger, new Fields())
        {
        }

        public Entry(Logger logger, Fields data)
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

            Logger.Hooks.Fire(this);
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
            var newData = new Fields(Data) { { key, value } };
            return new Entry(Logger, newData);
        }

        public Entry WithFields(Dictionary<string, object> fields)
        {
            var newData = new Fields(fields.Count + Data.Count);
            foreach (var keyvalpair in fields.Concat(Data))
            {
                newData.Add(keyvalpair.Key, keyvalpair.Value);
            }
            return new Entry(Logger, newData);
        }
    }
}
