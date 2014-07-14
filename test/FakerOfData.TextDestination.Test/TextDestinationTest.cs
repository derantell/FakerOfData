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

            public Load_method() {
                testOutput = new StringBuilder();
                testWriter = s => new StringWriter(testOutput);
            }

            [Fact]
            public void should_use_the_specified_field_separator_character() {
                var options = new TextDestinationOptions(",");
                var destination = new TextDestination(options, testWriter);

                var data = new[] { 
                    new TestData { Foo = "Awesome", Bar = 42 }
                };

                destination.Load(data);

                var result = testOutput.ToString();

                Check.That(result).StartsWith("Awesome,42");

            }

            [Fact]
            public void should_use_the_specified_line_separator_character_to_separate_lines() {
                var options = new TextDestinationOptions(lineSeparator: "$$");
                var destination = new TextDestination(options, testWriter);

                var data = new[] {new {Foobar = "Foobar"}};

                destination.Load(data);

                var result = testOutput.ToString();

                Check.That(result).EndsWith("$$");
            }

            [Fact]
            public void should_add_header_fields_as_first_row_when_specified_in_options() {
                var options = new TextDestinationOptions( firstLineIsHeaders: true );
                var destination = new TextDestination(options, testWriter);

                var data = new[] {
                    new TestData {Foo = "Hello world", Bar = 42}
                };

                destination.Load(data);

                var result = testOutput.ToString();

                Check.That(result).StartsWith("Foo\tBar");
            }

            [Fact]
            public void should_not_add_header_fields_as_first_row_when_that_flag_is_false() {
                var options = new TextDestinationOptions(firstLineIsHeaders:false);
                var destination = new TextDestination(options, testWriter);

                var data = new[] {
                    new TestData {Foo = "Hello world", Bar = 42}
                };

                destination.Load(data);

                var result = testOutput.ToString();

                Check.That(result).StartsWith("Hello world\t42");
            }


            [Fact]
            public void should_not_split_header_names_when_split_header_option_is_cleared() {
                var options = new TextDestinationOptions(firstLineIsHeaders: true, splitHeaderText: false);

                var destination = new TextDestination(options, testWriter);

                var data = new[] {
                    new {
                        Field_no_1 = "foo",
                        FieldNo2 = "bar"
                    }
                };

                destination.Load(data);

                var result = testOutput.ToString();

                Check.That(result).StartsWith("Field_no_1\tFieldNo2");
            
            }

            [Fact]
            public void should_split_header_labels_on_underscore_when_split_header_options_is_set() {
                var options = new TextDestinationOptions(firstLineIsHeaders: true, splitHeaderText: true);

                var destination = new TextDestination(options, testWriter);

                var data = new[] {
                    new { Field_no_1 = "foo" }
                };

                destination.Load(data);

                var result = testOutput.ToString();

                Check.That(result).StartsWith("Field no 1");
            }

            [Fact]
            public void should_split_header_on_camel_case_when_split_flag_is_set_and_name_does_not_contain_underscores() {
                var options = new TextDestinationOptions(firstLineIsHeaders: true, splitHeaderText: true);

                var destination = new TextDestination(options, testWriter);

                var data = new[] {
                    new {FooBarBaz = "foobar"}
                };

                destination.Load(data);

                var result = testOutput.ToString();

                Check.That(result).StartsWith("Foo Bar Baz");
            }

            [Fact]
            public void should_order_fields_as_specified_by_the_fieldorder_option() {
                var options = new TextDestinationOptions(
                    firstLineIsHeaders: true,
                    fieldOrder: new[] {"Bar", "Baz", "Foo"} );

                var destination = new TextDestination(options, testWriter);

                var data = new[] {
                    new {Foo = "foo", Bar = "bar", Baz = "baz"}
                };

                destination.Load(data);

                var result = testOutput.ToString();

                Check.That(result)
                    .StartsWith("Bar\tBaz\tFoo")
                    .And
                    .EndsWith("bar\tbaz\tfoo\r\n");
            }

            [Fact]
            public void should_skip_fields_that_are_missing_from_fieldorder_option() {
                var options = new TextDestinationOptions(
                    firstLineIsHeaders: true,
                    fieldOrder: new[] {"Bar", "Baz"} );

                var destination = new TextDestination(options, testWriter);

                var data = new[] {
                    new {Foo = "foo", Bar = "bar", Baz = "baz"}
                };

                destination.Load(data);

                var result = testOutput.ToString();

                Check.That(result)
                    .StartsWith("Bar\tBaz")
                    .And
                    .EndsWith("bar\tbaz\r\n");
            }

            [Fact]
            public void should_quote_strings_containing_field_line_or_quote_chars_when_quote_values_flag_is_set() {
                var options = new TextDestinationOptions( 
                    quoteFields: true,
                    quoteCharacter: "'",
                    fieldSeparator: ",",
                    lineSeparator: "$");

                var destination = new TextDestination(options, testWriter);

                var data = new[] {
                    new {
                        Quote = "'Quoted'", 
                        Field = "Fi,eld", 
                        Line = "Li$ne"
                    }
                };

                destination.Load(data);

                var result = testOutput.ToString();

                Check.That(result)
                    .StartsWith("'''Quoted''','Fi,eld','Li$ne'$");
            }
        }


        public class TestData {
            public string Foo { get; set; }
            public int Bar { get; set; }
        }
    }
}
