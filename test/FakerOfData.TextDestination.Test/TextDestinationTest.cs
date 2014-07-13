using System;
using System.IO;
using System.Text;
using NFluent;
using Xunit;

namespace FakerOfData.TextDestination.Test {
    public class TextDestinationTest {

        public class Load_method {
            private readonly StringBuilder testOutput;
            private readonly Func<string,TextWriter> testWriter;
            private readonly TextDestinationOptions testOptions;

            public Load_method() {
                testOutput = new StringBuilder();
                testWriter = s => new StringWriter(testOutput);
                testOptions = TextDestinationOptions.Default;
            }

            [Fact]
            public void should_add_header_fields_as_first_row_when_specified_in_options() {
                testOptions.FirstLineIsHeaders = true;
                var destination = new TextDestination(testOptions, testWriter);

                var data = new[] {
                    new TestData {Foo = "Hello world", Bar = 42}
                };

                destination.Load(data);

                var result = testOutput.ToString();

                Check.That(result).StartsWith("Foo\tBar");
            }

            [Fact]
            public void should_not_add_header_fields_as_first_row_when_that_flag_is_false() {
                testOptions.FirstLineIsHeaders = false;
                var destination = new TextDestination(testOptions, testWriter);

                var data = new[] {
                    new TestData {Foo = "Hello world", Bar = 42}
                };

                destination.Load(data);

                var result = testOutput.ToString();

                Check.That(result).StartsWith("Hello world\t42");
                            
            }
        }

        public class TestData {
            public string Foo { get; set; }
            public int Bar { get; set; }
        }
    }
}
