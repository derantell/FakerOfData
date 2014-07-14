using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using FakerOfData.Core.Utility;

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
            var properties = OrderProperties( typeof (T).GetProperties() );

            using (var writer = _createWriter(filename)) {
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
            var fields = properties.Select(property => property.GetValue(item).ToNullString());
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

        private string SplitHeaderName(PropertyInfo property) {
            if (!_options.SplitHeaderText) return property.Name;

            return property.Name.Contains("_") 
                ? property.Name.Replace("_", " ")
                : DeCamelizer.Split(property.Name);
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

    public struct TextDestinationOptions {
        public TextDestinationOptions(
            string fieldSeparator = "\t",
            string lineSeparator = "\r\n",
            bool firstLineIsHeaders = false,
            bool splitHeaderText = false,
            string[] fieldOrder = null 
        ) {
            FirstLineIsHeaders = firstLineIsHeaders;
            SplitHeaderText = splitHeaderText;
            FieldSeparator = fieldSeparator;
            LineSeparator = lineSeparator;
            FieldOrder = fieldOrder ?? new string[0];
        }

        public readonly bool FirstLineIsHeaders;
        public readonly bool SplitHeaderText;
        public readonly string FieldSeparator;
        public readonly string LineSeparator;
        public readonly string[] FieldOrder;

        public static readonly TextDestinationOptions Default = new TextDestinationOptions();
    }
}