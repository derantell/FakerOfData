using FakerOfData.Common.RandomValues;
using NFluent;
using Xunit;

namespace FakerOfData.Common.Test.RandomValues {
    public class The_RandomStringPatternValue_class {

        public class Key_property {

            [Fact]
            public void should_return_the_value_StringPattern() {
                var stringPattern = new RandomStringPatternValue();

                Check.That(stringPattern.Key).IsEqualTo("StringPattern");
            }
        }

        public class RandomValues_method {
            private readonly RandomStringPatternValue stringPattern;
            public RandomValues_method() {
                stringPattern = new RandomStringPatternValue();
            }

            [Fact]
            public void should_return_the_empty_string_when_no_pattern_is_specified() {
                Check.That((string)stringPattern.RandomValues(null)).IsEmpty();
            }

            [Fact]
            public void should_return_the_pattern_string_when_it_contains_no_choices() {
                var pattern = new {pattern = "foobar"};

                var value = stringPattern.RandomValues(pattern);

                Check.That(value).IsEqualTo("foobar");
            }

            [Fact]
            public void should_return_a_string_with_letters_from_only_the_specified_character_class() {
                var pattern = new {pattern = "[abc]"};

                var value = (string)stringPattern.RandomValues(pattern);

                Check.That(value).Matches("^[abc]$");
            }


            [Fact]
            public void should_return_a_string_of_specified_length() {
                var pattern = new {pattern = "[abc]{3}"};

                var value = (string)stringPattern.RandomValues(pattern);

                Check.That(value).Matches("^[abc]{3}$");
            }

            [Fact]
            public void should_return_a_string_with_length_between_specified_by_min_and_max_value() {
                var pattern = new {pattern = "[abc]{3,3}"};

                var value = (string)stringPattern.RandomValues(pattern);

                Check.That(value).Matches("^[abc]{3,3}$");
            }

            [Fact]
            public void should_join_the_results_of_all_pattern_parts() {
                var pattern = new {pattern = "FOO-[1234]{3,5}-BAR"}; 

                var value = (string) stringPattern.RandomValues(pattern);

                Check.That(value).Matches("^FOO-[1234]{3,5}-BAR$");
            }
        }
    }
}
