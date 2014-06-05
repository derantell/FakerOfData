using NFluent;
using Xunit;

namespace FakerOfData.Test {
    public class CounterTest {

        public class Next_property {

            [Fact]
            public void should_dynamically_create_a_new_counter_when_child_property_is_accessed() {
                int id = Counter.Next.Id;
                int index = Counter.Next.Index;

                Check.That(id).IsEqualTo(index);
            }

            [Fact]
            public void should_set_a_counter_to_a_specified_value() {
                var fortytwo = 42;

                Counter.Next.MeaningOfLife = i;

                Check.That(Counter.Next.MeaningOfLife).IsEqualTo(fortytwo);
            }
        }
    }
}
