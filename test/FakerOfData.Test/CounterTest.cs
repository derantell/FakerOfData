using NFluent;
using Xunit;

namespace FakerOfData.Test {
    public class CounterTest {

        public class Next_property {

            [Fact]
            public void should_dynamically_create_a_new_counter_when_child_property_is_accessed() {
                int id    = Counter.Next.Id;
                int index = Counter.Next.Index;

                Check.That(id).IsEqualTo(index);
            }

            [Fact]
            public void should_set_a_counter_to_a_specified_value() {
                var fortytwo = 42;

                Counter.Next.MeaningOfLife = fortytwo;

                Check.That((int)Counter.Next.MeaningOfLife).IsEqualTo(fortytwo);
            }

            [Fact]
            public void should_increment_the_counter_by_one_after_each_access() {
                Counter.Next.Increment = 1;

                int one   = Counter.Next.Increment;
                int two   = Counter.Next.Increment;
                int three = Counter.Next.Increment;

                Check.That(new[] {one, two, three}).ContainsExactly(1, 2, 3);
            }
        }
    }
}
