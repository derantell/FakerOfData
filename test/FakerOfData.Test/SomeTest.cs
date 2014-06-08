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

        public class ItemFrom_method {
            [Fact]
            public void should_return_a_random_item_from_a_sequence() {
                var random = new Random();
                var data = new[] {1, 2, 3};

                var item = random.ItemFrom(data);

                Check.That(item).IsGreaterThan(0).And.IsLessThan(4);
            }
        }
    }
}
