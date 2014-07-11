using FakeItEasy;
using NFluent;
using Xunit;

namespace FakerOfData.Test {
    public class FileStringSourceTest {

        public class GetStrings_method {

            [Fact]
            public void should_try_to_find_file_in_the_configured_directory_matching_the_specified_key() {
                var source = new FileStringSource("Strings");

                var strings = source.GetStrings("FooBar");

                Check.That(strings).ContainsExactly(new[] {"Foo", "Bar", "Baz"});
            }
        }
    }
}
