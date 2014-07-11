using System.Collections.Generic;
using FakeItEasy;
using NFluent;
using Xunit;

namespace FakerOfData.Test {
    public class The_RandomStringValue_class {

        class TestStringSource : IStringSource {
            public IEnumerable<string> Keys {
                get { return new[] {"Foo"}; }
            }

            public IEnumerable<string> GetStrings(string key) {
                return new[] {"Bar", "Baz", "Quux"};
            }
        }

        public class Key_property {
            [Fact]
            public void should_return_the_name_of_the_string_collection_from_which_strings_are_drawn() {
                var value = new RandomStringValue("Foo", new TestStringSource());

                Check.That(value.Key).IsEqualTo("Foo");
            }
        }

        public class RandomValues_method {
            [Fact]
            public void should_return_a_random_string_from_the_specified_collection() {
                var values = new RandomStringValue("Foo", new TestStringSource());

                var value = (string) values.RandomValues(new {});

                Check.That(value).Matches(@"^(Bar|Baz|Quux)$");
            }
        }
    }
}
