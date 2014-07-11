using FakeItEasy;
using NFluent;
using Xunit;

namespace FakerOfData.Test {
    public class The_static_Draw_class {

        public class One_method {

            [Fact]
            public void should_pick_a_random_value_from_the_specified_array() {
                var value = (string) Draw.One("foo", "bar", "baz");

                Check.That(value).Matches("^(foo|bar|baz)$");
            }
        }

        public class DrawRandomItem_extension_method {
            [Fact]
            public void should_draw_a_random_value_from_the_collection_on_which_it_is_called() {
                var values = new[] {"foo", "bar", "baz"};

                var value = values.DrawRandomItem();

                Check.That(value).Matches("^(foo|bar|baz)$");
            }
        }
    }
}
