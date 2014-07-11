using System.Collections;
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
        }

        public class Dummy {};

    }
}
