using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FakerOfData.TextDestination {
    public class TextDestination : IDestination {
        public TextDestination(string separator, string outputPath) 
            : this(separator, f => new StreamWriter(Path.Combine(outputPath,f))) { }

        public TextDestination(string separator, Func<string, TextWriter> writerCreator = null ) {
            _createWriter = writerCreator ?? DefaultWriter;
            _separator = separator;
        }

        public IEnumerable<T> Load<T>(IEnumerable<T> sequence) {
            var filename = string.Format(FileNameTemplate, typeof (T).Name, DateTime.Now);
            using (var writer = _createWriter(filename)) {
                foreach (var item in sequence) {
                    writer.WriteLine( BuildLine(item) );
                }
            }
            return sequence;
        }

        private string BuildLine<T>(T item) {
            var properties = typeof (T).GetProperties();
            var fields = properties.Select(property => property.GetValue(item).ToNullString());
            return string.Join(_separator, fields);
        }

        private TextWriter DefaultWriter(string filePath) {
            return new StreamWriter(filePath);
        }

        private const string FileNameTemplate = "{0}-{1:yyyyMMddHHmmss}.txt";
        private readonly Func<string, TextWriter> _createWriter;
        private readonly string _separator;
    }

    static class Extensions {
        public static string ToNullString(this object self, string nullValue = "<null>") {
            return (self ?? nullValue).ToString();
        }
    }
}