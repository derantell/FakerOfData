using System;
using NFluent;
using Xunit;

namespace FakerOfData.Test {
    public class SomeTest {

        public class Random_property {

            [Fact]
            public void should_return_a_random_generator() {
                var random = Some.Random;
                // This is a really stupid test, isn't it?
                Check.That(random).IsInstanceOf<Random>();
            }
        }
    }
}
