using Larch.Lib.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Larch.Lib
{
    public class Larch
    {
        public LevelHooks Hooks { get; set; }

        public Encoding Encoding { get; set; }

        public IFormatter Formatter { get; set; }

        public static Larch DefaultLogger()
        {
            var logger = new Larch();
            logger.Hooks.Add(new ConsoleOutputAdapter(LevelExtensions.GetLevels(Level.DebugLevel)));
            return logger;
        }

        public Larch()
        {
            Encoding = Encoding.UTF8;
            Hooks = new LevelHooks();
            Formatter = new TextFormatter();
        }

        private Entry NewEntry()
        {
            // TODO: maybe use caching like in logrus in the future
            return new Entry(this);
        }

        private Entry NewEntry(Fields fields)
        {
            return new Entry(this, fields);
        }

        public Entry WithField(string key, object value)
        {
            return NewEntry().WithField(key, value);
        }

        public Entry WithFields(Fields fields)
        {
            return NewEntry(fields);
        }

        public Entry WithException(Exception ex)
        {
            return NewEntry().WithException(ex);
        }

        public void Debug(params object[] objects) => NewEntry().Debug(objects);
        public void Info(params object[] objects) => NewEntry().Info(objects);
        public void Warn(params object[] objects) => NewEntry().Warn(objects);
        public void Error(params object[] objects) => NewEntry().Error(objects);
        public void Fatal(params object[] objects) => NewEntry().Fatal(objects);
    }
}
