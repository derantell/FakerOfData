using System;
using System.Collections.Generic;
using FakeItEasy;
using NFluent;
using Xunit;

namespace FakerOfData.Test {

    public class The_static_Some_class {
        public class TestRandomValue : IRandomValue {
            public Func<object, object> RandomValues { get; set; }
            public string Key { get; set; }
        }

        public class RandomThing_method_with_key_and_func_parameters {
            public RandomThing_method_with_key_and_func_parameters() {
                Some.Clear();
            }

            [Fact]
            public void should_register_the_specified_random_value_function_under_the_specified_key() {
                Some.RandomThing("foo", _ => "Bar");

                var randomValue = Some.RandomValue<string>("foo");

                Check.That(randomValue).IsEqualTo("Bar");
            }
        }

        public class RandomThing_method_with_IRandomValue_parameter {
            public RandomThing_method_with_IRandomValue_parameter() {
                Some.Clear();
            }

            [Fact]
            public void should_register_the_random_value_implementation() {
                var testRandomValue = new TestRandomValue {
                    Key = "foo",
                    RandomValues = _ => "Baz"
                };

                Some.RandomThing(testRandomValue);

                var randomValue = Some.RandomValue<string>("foo");

                Check.That(randomValue).IsEqualTo("Baz");
            }
        }

        public class RandomThings_method_with_IRandomValue_params {
            public RandomThings_method_with_IRandomValue_params() {
                Some.Clear();
            }

            [Fact]
            public void should_register_each_specified_IRandomValue() {
                Some.RandomThings(
                    new TestRandomValue{Key = "foo", RandomValues = _=>"Foo"},
                    new TestRandomValue{Key = "bar", RandomValues = _=>"Bar"},
                    new TestRandomValue{Key = "baz", RandomValues = _=>"Baz"}
                );

                var randomValues = new[] {
                    Some.RandomValue<string>("foo"),
                    Some.RandomValue<string>("bar"),
                    Some.RandomValue<string>("baz"),
                };

                Check
                    .That(randomValues)
                    .ContainsExactly(new[] {"Foo", "Bar", "Baz"});
            }
        }

        public class RandomThings_method_with_IRandomValue_sequence_parameter {
            public RandomThings_method_with_IRandomValue_sequence_parameter() {
                Some.Clear();
            }

            [Fact]
            public void should_register_all_IRandomValues_in_sequence() {
                var randomThings = new List<IRandomValue> {
                    new TestRandomValue {Key = "foo", RandomValues = _ => "Foo"},
                    new TestRandomValue {Key = "bar", RandomValues = _ => "Bar"},
                    new TestRandomValue {Key = "baz", RandomValues = _ => "Baz"}
                };

                Some.RandomThings(randomThings);

                var randomValues = new[] {
                    Some.RandomValue<string>("foo"),
                    Some.RandomValue<string>("bar"),
                    Some.RandomValue<string>("baz"),
                };

                Check
                    .That(randomValues)
                    .ContainsExactly(new[] {"Foo", "Bar", "Baz"});
            }
        }

        public class RandomValue_method {
            public RandomValue_method() {
                Some.Clear();
            }

            [Fact]
            public void should_return_the_value_of_a_call_to_the_function_registered_under_the_specified_key() {
                Some.RandomThing("foo", _ => "Foo");

                var randomValue = Some.RandomValue<string>("foo");

                Check.That(randomValue).IsEqualTo("Foo");
            }

            [Fact]
            public void should_pass_the_specified_options_object_to_the_registered_function() {
                Some.RandomThing("foo", opt => opt);

                var randomValue = Some.RandomValue<string>("foo", "braz");

                Check.That(randomValue).IsEqualTo("braz");
            }
        }

        public class Random_property {
            public Random_property() {
                Some.Clear();
            }

            [Fact]
            public void should_be_a_RandomThings_dynamic_collection() {
                Check.That((object)Some.Random).IsInstanceOf<RandomThings>();
            }

            [Fact]
            public void registered_func_should_be_callable_as_a_property() {
                Some.Random.Foo = new Func<object, object>(_ => "foo");

                string value = Some.Random.Foo;

                Check.That(value).IsEqualTo("foo");
            }

            [Fact]
            public void registered_func_should_be_callable_as_a_method() {
                Some.Random.Foo = new Func<object, object>(_ => "foo");

                string value = Some.Random.Foo();

                Check.That(value).IsEqualTo("foo");
            }

            [Fact]
            public void should_pass_options_object_to_registered_func_when_called_as_method() {
                Some.Random.Foo = new Func<object, object>(opt => opt);

                string value = Some.Random.Foo("Brazza");

                Check.That(value).IsEqualTo("Brazza");
            }
        }
    }
}
