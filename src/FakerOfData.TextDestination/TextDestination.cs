using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FakerOfData.Core.Utility;

namespace FakerOfData.TextDestination {
    public class TextDestination : IDestination {
        public TextDestination(TextDestinationOptions options, string outputPath) 
            : this(options, () => new StreamWriter(Path.Combine(outputPath))) { }

        public TextDestination(TextDestinationOptions options, Func<TextWriter> writerCreator = null ) {
            _createWriter = writerCreator ?? DefaultWriter;
            _options = options;
        }

        public IEnumerable<T> Load<T>(IEnumerable<T> sequence) {
            var filename = string.Format(FileNameTemplate, typeof (T).Name, DateTime.Now);
            var properties = OrderProperties( typeof (T).GetProperties() );

            using (var writer = _createWriter()) {
                if (_options.FirstLineIsHeaders) {
                    writer.Write(BuildHeaders(properties) + _options.LineSeparator);
                }
                foreach (var item in sequence) {
                    writer.Write( BuildLine(item,properties) + _options.LineSeparator );
                }
            }
            return sequence;
        }

        private string BuildHeaders(IEnumerable<PropertyInfo> properties) {
            var fields = properties.Select(SplitHeaderName);
            return string.Join(_options.FieldSeparator, fields);
        }

        private string BuildLine<T>(T item, IEnumerable<PropertyInfo> properties ) {
            var fields = properties.Select(property => Quote( property.GetValue(item).ToNullString()));
            return string.Join(_options.FieldSeparator, fields);
        }

        private PropertyInfo[] OrderProperties(IEnumerable<PropertyInfo> properties ) {
            if (_options.FieldOrder.Length == 0) return properties.ToArray();

            var ordered = _options.FieldOrder
                .Select(name => properties.FirstOrDefault(p => name.Equals(p.Name, StringComparison.OrdinalIgnoreCase)))
                .Where(p => p != null)
                .ToArray();

            return ordered;
        }

        private string Quote(string value) {
            if (!_options.QuoteFields) return value;

            if (value.Contains(_options.QuoteCharacter) ||
                value.Contains(_options.FieldSeparator) ||
                value.Contains(_options.LineSeparator)) {

                value = value.Replace(_options.QuoteCharacter, _options.QuoteCharacter + _options.QuoteCharacter);
                value = string.Format("{1}{0}{1}", value, _options.QuoteCharacter);
            }

            return value;
        }

        private string SplitHeaderName(PropertyInfo property) {
            if (!_options.SplitHeaderText) return property.Name;

            return property.Name.Contains("_") 
                ? property.Name.Replace("_", " ")
                : DeCamelizer.Split(property.Name);
        }

        private TextWriter DefaultWriter() {
            var filePath = string.Format(FileNameTemplate, "fakedata", DateTime.Now);
            return new StreamWriter(filePath);
        }

        private const string FileNameTemplate = "{0}-{1:yyyyMMddHHmmss}.txt";
        private readonly Func<TextWriter> _createWriter;
        private readonly TextDestinationOptions _options;
    }
}