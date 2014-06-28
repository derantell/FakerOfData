using System;
using System.Collections.Generic;
using FakeItEasy;
using NFluent;
using Xunit;

namespace FakerOfData.Test {
    public class The_RandomThings_dynamic_class {

        public class TryGetMember_overridden_method {

            [Fact]
            public void should_throw_an_exception_when_accessing_a_non_existing_property() {
                dynamic randomThings = new RandomThings();

                Check.ThatCode(() => randomThings.Foo).Throws<KeyNotFoundException>();
            }

            [Fact]
            public void should_get_a_value_from_the_function_registered_under_the_specified_key() {
                var randomThings = new RandomThings();

                var result = new {};
                randomThings.RegisterValues("Test", _ => result);

                dynamic dynamicThings = randomThings;

                Check.That((object) dynamicThings.Test).IsSameReferenceThan(result);
            }
        }

        public class TrySetMember_overridden_method {
            [Fact]
            public void should_set_the_function_of_the_specified_key_when_called_dynamically() {
                dynamic randomThings = new RandomThings();

                var result = new {};
                randomThings.Test = new Func<object, object>(_ => result);

                Check.That((object) randomThings.Test).IsSameReferenceThan(result);
            }

        }
    }
}
