using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Larch.Lib.Contracts;

namespace Larch.Lib
{
    public class RollingFileOutputAdapter : IHook
    {
        private readonly Level[] _levels;
        private readonly string _baseFilename;
        private readonly int _maxFileSize;
        private readonly int _maxFilesCount;
        private volatile object _lock = new object();
        private FileStream _currentFile;

        public RollingFileOutputAdapter(Level[] levels, string baseFilename, int maxFileSize, int maxFilesCount)
        {
            _levels = levels;
            _baseFilename = baseFilename;
            _maxFileSize = maxFileSize;
            _maxFilesCount = maxFilesCount;
        }

        public Level[] Levels()
        {
            return _levels;
        }

        public void Fire(Entry entry)
        {
            var log = entry.Logger.Formatter.Format(entry);
            lock (_lock)
            {
                if (_currentFile == null)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(_baseFilename));
                    _currentFile = OpenWrite(_baseFilename);
                    if (_currentFile.Length > 0) _currentFile.Seek(_currentFile.Length, SeekOrigin.Begin);
                }
                if (_currentFile.Length >= _maxFileSize)
                {
                    Roll();
                }
                var buf = Encoding.UTF8.GetBytes($"{log}\n");
                _currentFile.Write(buf, 0, buf.Length);
                _currentFile.Flush();
            }
        }

        private FileStream OpenWrite(string filename)
        {
            return new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
        }

        private void Roll()
        {
            _currentFile.Dispose();
            var newFilename = Path.Combine(
                Path.GetDirectoryName(_baseFilename),
                $"{Path.GetFileNameWithoutExtension(_baseFilename)}_{DateTime.Now:yyyy-MM-ddTHH-mm-ss}{Path.GetExtension(_baseFilename)}");
            File.Move(_baseFilename, newFilename);
            _currentFile = OpenWrite(_baseFilename);
            DeleteOld();
        }

        private void DeleteOld()
        {
            var dir = Path.GetDirectoryName(_baseFilename);
            var files = Directory.GetFiles(dir);
            var sortedByDate = files.Select(f => (f, ParseDateTime(f)))
                .Where(t => t.Item2.HasValue)
                .OrderBy(t => t.Item2)
                .ToList();
            var count = sortedByDate.Count;
            if (count <= _maxFilesCount) return;
            var toDel = count - _maxFilesCount;
            foreach (var (f, _) in sortedByDate.Take(toDel))
            {
                File.Delete(f);
            }
        }

        private DateTime? ParseDateTime(string filename)
        {
            filename = Path.GetFileName(filename);
            var match = Regex.Match(filename, ".*?_(\\d{4}-\\d{2}-\\d{2}T\\d{2}-\\d{2}-\\d{2})\\..*");
            if (!match.Success) return null;
            if (DateTime.TryParseExact(match.Groups[1].Value, "yyyy-MM-ddTHH-mm-ss", new DateTimeFormatInfo(), DateTimeStyles.None, out DateTime res))
            {
                return res;
            }
            return null;
        }
    }
}
