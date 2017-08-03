using Larch.Lib.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Larch.Lib
{
    public class Larch
    {
        public ConcurrentBag<IHook> Hooks { get; set; }

        private IOutputAdapter _out = new ConsoleOutputAdapter();
        private Encoding _encoding = Encoding.UTF8;

        public Encoding Encoding
        {
            get
            {
                _encodingLock.EnterReadLock();
                var e = _encoding;
                _encodingLock.ExitReadLock();
                return e;
            }
            set
            {
                _encodingLock.EnterWriteLock();
                _encoding = value;
                _encodingLock.ExitWriteLock();
            }
        }



        private IFormatter _formatter = new TextFormatter();

        public void SetOutputAdapter(IOutputAdapter outputAdapter)
        {
            lock (_outputLock)
            {
                _out = outputAdapter;
            }
        }

        public void WriteToOutput(string log)
        {
            lock (_outputLock)
            {
                _out.Write(log);
            }
        }

        public IFormatter Formatter
        {
            get
            {
                _formatterLock.EnterReadLock();
                var f = _formatter;
                _formatterLock.ExitReadLock();
                return f;
            }
            set
            {
                _formatterLock.EnterWriteLock();
                _formatter = value;
                _formatterLock.ExitWriteLock();
            }
        }

        private readonly ReaderWriterLockSlim _formatterLock = new ReaderWriterLockSlim();
        private volatile object _outputLock = new object();
        private readonly ReaderWriterLockSlim _encodingLock = new ReaderWriterLockSlim();

        private Entry NewEntry()
        {
            var entry = new Entry(this);
            return entry;
        }

        public Entry WithField(string key, object value)
        {
            return new Entry(this).WithField(key, value);
        }

        public Entry WithFields(Fields fields)
        {
            return new Entry(this, fields);
        }

        public Entry WithException(Exception ex)
        {
            return new Entry(this).WithException(ex);
        }

        public void Debug(params object[] objects) => new Entry(this).Debug(objects);
        public void Info(params object[] objects) => new Entry(this).Info(objects);
        public void Warn(params object[] objects) => new Entry(this).Warn(objects);
        public void Error(params object[] objects) => new Entry(this).Error(objects);
        public void Fatal(params object[] objects) => new Entry(this).Fatal(objects);
    }
}
