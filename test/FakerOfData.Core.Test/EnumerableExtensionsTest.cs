using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NFluent;
using Xunit;

namespace FakerOfData.Test {
    public class EnumerableExtensionsTest {

        public class Load_method {

            [Fact]
            public void should_call_the_configured_destination_on_the_generator() {
                var fakeDestination = A.Fake<IDestination>();
                Generator.Destination = fakeDestination;

                Generator.Of<Dummy>().Take(1).Load();

                A.CallTo(() => fakeDestination.Load(A<IEnumerable<Dummy>>._))
                    .MustHaveHappened();
            }

            [Fact]
            public void should_return_the_collection_returned_by_the_destination() {
                var expected = new Dummy[0];
                var fakeDestination = A.Fake<IDestination>();
                A.CallTo(() => fakeDestination.Load(expected))
                    .Returns(expected);
                Generator.Destination = fakeDestination;

                var actual = expected.Load();
                Check.That(actual).IsSameReferenceThan(expected);
            }
        }

        public class Dummy {};

    }
}
