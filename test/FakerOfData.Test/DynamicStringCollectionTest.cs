using System.Collections.Generic;
using FakeItEasy;
using NFluent;
using Xunit;

namespace FakerOfData.Test {
    public class DynamicStringCollectionTest {
        public class TryGetMember {

            [Fact]
            public void should_try_to_get_the_collection_from_the_configured_string_source_on_first_access() {
                var fakeSource = A.Fake<IStringSource>();
                dynamic collection = new DynamicStringCollection(fakeSource);

                IEnumerable<string> strings = collection.FooBar;

                A.CallTo(() => fakeSource.GetStrings("FooBar")).MustHaveHappened();
            }

            [Fact]
            public void should_cache_the_result_of_a_collection_on_first_access() {
                var fakeSource = A.Fake<IStringSource>();
                dynamic collection = new DynamicStringCollection(fakeSource);

                IEnumerable<string> strings1 = collection.FooBar;
                IEnumerable<string> strings2 = collection.FooBar;

                Check.That(strings1).IsSameReferenceThan(strings2);
            }
        }

        public class TrySetMember {
            [Fact]
            public void should_set_the_collection_to_the_specified_list() {
                var fakeSource = A.Fake<IStringSource>();
                dynamic collection = new DynamicStringCollection(fakeSource);

                var strings = new[] {"Foo", "Bar"};
                collection.BarFoo = strings;

                Check.That((string[])collection.BarFoo).IsSameReferenceThan(strings);
            }
        }
    }
}