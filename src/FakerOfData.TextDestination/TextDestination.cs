using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FakerOfData.TextDestination {
    public class TextDestination : IDestination {
        public TextDestination(TextDestinationOptions options, string outputPath) 
            : this(options, f => new StreamWriter(Path.Combine(outputPath,f))) { }

        public TextDestination(TextDestinationOptions options, Func<string, TextWriter> writerCreator = null ) {
            _createWriter = writerCreator ?? DefaultWriter;
            _options = options;
        }

        public IEnumerable<T> Load<T>(IEnumerable<T> sequence) {
            var filename = string.Format(FileNameTemplate, typeof (T).Name, DateTime.Now);
            var properties = typeof (T).GetProperties();
            using (var writer = _createWriter(filename)) {
                if (_options.FirstLineIsHeaders) {
                    writer.WriteLine(BuildHeaders(properties));
                }
                foreach (var item in sequence) {
                    writer.WriteLine( BuildLine(item,properties) );
                }
            }
            return sequence;
        }

        private string BuildHeaders(IEnumerable<PropertyInfo> properties) {
            var fields = properties.Select(SplitHeaderName);
            return string.Join(_options.FieldSeparator, fields);
        }

        private string BuildLine<T>(T item, IEnumerable<PropertyInfo> properties ) {
            var fields = properties.Select(property => property.GetValue(item).ToNullString());
            return string.Join(_options.FieldSeparator, fields);
        }

        private string SplitHeaderName(PropertyInfo property) {
            if (_options.SplitHeaderText) { 
                if (property.Name.Contains("_"))
                    return property.Name.Replace("_", " ");
            }
            return property.Name;
        }

        private TextWriter DefaultWriter(string filePath) {
            return new StreamWriter(filePath);
        }

        private const string FileNameTemplate = "{0}-{1:yyyyMMddHHmmss}.txt";
        private readonly Func<string, TextWriter> _createWriter;
        private readonly TextDestinationOptions _options;
    }

    static class Extensions {
        public static string ToNullString(this object self, string nullValue = "<null>") {
            return (self ?? nullValue).ToString();
        }
    }

    public class TextDestinationOptions {
        public bool FirstLineIsHeaders { get; set; }
        public bool SplitHeaderText { get; set; }
        public string FieldSeparator { get; set; }

        public static readonly TextDestinationOptions Default = new TextDestinationOptions {
            FirstLineIsHeaders = false,
            FieldSeparator = "\t"
        };
    }
}