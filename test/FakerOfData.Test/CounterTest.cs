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
                var start = 41;

                Counter.Next.MeaningOfLife = start;

                Check.That((int)Counter.Next.MeaningOfLife).IsEqualTo(42);
            }

            [Fact]
            public void should_increment_the_counter_by_one_after_each_access() {
                int one   = Counter.Next.Increment;
                int two   = Counter.Next.Increment;
                int three = Counter.Next.Increment;

                Check.That(new[] {one, two, three}).ContainsExactly(one, one+1, one+2);
            }
        }

        public class NextFor_method {
            [Fact]
            public void should_return_the_next_increment_of_the_named_counter() {
                var index1 = Counter.NextFor("index");
                var index2 = Counter.NextFor("index");
                var index3 = Counter.NextFor("index");

                Check.That(new[] {index1, index2, index3}).ContainsExactly(index1, index1+1, index1+2);
            }
        }

        public class SetFor_method {
            [Fact]
            public void should_set_the_value_of_the_named_counter() {
                Counter.SetFor("id", 41);

                var fortyTwo = Counter.NextFor("id");

                Check.That(fortyTwo).IsEqualTo(42);
            }
        }
    }
}
