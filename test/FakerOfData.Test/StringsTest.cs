using System;
using FakeItEasy;
using NFluent;
using Xunit;

namespace FakerOfData.Test {
    public class StringsTest {

        public class String_method {

            [Fact]
            public void should_get_a_random_string_from_the_generators_string_collection() {
                var fakeSource = A.Fake<IStringSource>();
                A.CallTo(() => fakeSource.GetStrings("foo")).Returns(new[] {"foo", "bar", "baz"});

                Generator.Strings = new DynamicStringCollection(fakeSource);

                var random = new Random();

                var value = random.String("foo");

                Check.That(value).Matches("foo|bar|baz");
            }
        }
    }
}
