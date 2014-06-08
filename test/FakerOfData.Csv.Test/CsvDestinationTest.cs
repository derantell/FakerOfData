using System;
using System.IO;
using System.Text;
using NFluent;
using Xunit;

namespace FakerOfData.Csv.Test {
    public class CsvDestinationTest {

        public class Load_method {

            [Fact]
            public void should_write_the_data_to_a_file_using_the_specified_field_separator() {
                var result = new StringBuilder();
                Func<string, TextWriter> writerCreator = s => new StringWriter(result);
                var destination = new CsvDestination(";",writerCreator);

                var data = new[] {
                    new TestData {Bar = 1, Foo = "Hello"},
                    new TestData {Bar = 2, Foo = "World"},
                };

                destination.Load(data);

                Check.That(result.ToString())
                    .Contains("Hello;1")
                    .And.Contains("World;2");
            }
        }

        public class TestData {
            public string Foo { get; set; }
            public int Bar { get; set; }
        }
    }
}
