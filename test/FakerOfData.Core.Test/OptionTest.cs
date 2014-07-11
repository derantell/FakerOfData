using FakeItEasy;
using NFluent;
using Xunit;

namespace FakerOfData.Test {
    public class OptionTest {

        public class Get_method{

            [Fact]
            public void should_return_the_value_of_the_specified_property() {
                var options = new {Foo = 42};

                var value = options.Get<int>("Foo");

                Check.That(value).IsEqualTo(42);
            }

            [Fact]
            public void should_return_the_default_value_of_the_type_when_property_does_not_exist() {
                var options = new {Foo = "Bar"};

                var value = options.Get<int>("Bar");

                Check.That(value).IsEqualTo(0);
            }

            [Fact]
            public void should_return_the_specified_default_value_when_property_does_not_exist() {
                var options = new {Foo = "Bar"};

                var value = options.Get("Bar", 1337);

                Check.That(value).IsEqualTo(1337);
            }
        }
    }
}
