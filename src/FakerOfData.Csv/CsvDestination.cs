using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FakerOfData.Csv {
    public class CsvDestination : IDestination {
        public CsvDestination(string separator, Func<string, TextWriter> writerCreator = null ) {
            _createWriter = writerCreator ?? DefaultWriter;
            _separator = separator;
        }

        public void Load<T>(IEnumerable<T> sequence) {
            var filename = string.Format(FileNameTemplate, typeof (T).Name, DateTime.Now);
            using (var writer = _createWriter(filename)) {
                foreach (var item in sequence) {
                    writer.WriteLine( BuildLine(item) );
                }
            }
        }

        private string BuildLine<T>(T item) {
            var properties = typeof (T).GetProperties();
            var fields = properties.Select(property => property.GetValue(item).ToString());
            return string.Join(_separator, fields);
        }

        private TextWriter DefaultWriter(string filePath) {
            return new StreamWriter(filePath);
        }

        private const string FileNameTemplate = "{0}-{1:yyyyMMddHHmmss}.csv";
        private readonly Func<string, TextWriter> _createWriter;
        private readonly string _separator;
    }
}