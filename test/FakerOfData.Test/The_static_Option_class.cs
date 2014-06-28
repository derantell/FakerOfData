using NFluent;
using Xunit;

namespace FakerOfData.Test {
    public class The_static_Option_class {

        public class Get_extension_method {

            [Fact]
            public void should_return_the_default_value_of_the_type_when_requested_property_is_not_found_and_no_default_value_is_specified() {
                var options = new {};

                var value = options.Get<int>("foo");

                Check.That(value).IsEqualTo(0);
            }

            [Fact]
            public void should_return_the_specified_default_value_when_the_requested_property_is_not_found() {
                var options = new {};

                var value = options.Get("foo", 42);

                Check.That(value).IsEqualTo(42);
            }

            [Fact]
            public void should_do_a_case_insensitive_match_on_the_property_name() {
                var options = new {Foo = "bar"};

                var value = options.Get<string>("foo");

                Check.That(value).IsEqualTo("bar");
            }
        }
    }
}
