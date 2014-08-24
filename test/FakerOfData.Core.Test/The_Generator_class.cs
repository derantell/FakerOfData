using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using NFluent;
using Xunit;

namespace FakerOfData.Test {
    public class The_Generator_class {

        public class Of_method {

            [Fact]
            public void should_generate_a_sequence_of_the_specified_type() {
                var persons = Generator.Of<Person>()
                    .Take(3)
                    .ToList();

                Check.That(persons).HasSize(3);
            }

            [Fact]
            public void should_call_each_action_specified_as_arguments() {
                var date = DateTime.Now;
                var name = "John";

                var person = Generator.Of<Person>(
                        p => p.Name = name,
                        p => p.BirthDate = date
                    )
                    .First();

                Check.That(person).HasFieldsWithSameValues(new {Name = name, BirthDate = date});
            }
        }

        public class Use_IRandomValue_method {
            private class TestRandomValue : IRandomValue {
                public Func<object, object> RandomValues { get { return _ => _; } }
                public string Key { get { return GetType().Name; } }
            }

            [Fact]
            public void should_add_the_specified_IRandomValue_to_the_Some_classic_api() {
                Generator.Use<TestRandomValue>();

                var opt = new {};

                var classicResult = Some.RandomValue<object>("TestRandomValue", opt);

                Check.That(classicResult).IsSameReferenceThan(opt);
            }

            [Fact]
            public void should_add_the_specified_IRandomValue_to_the_Some_dynamic_api() {
                Generator.Use<TestRandomValue>();

                var opt = new {};

                var dynamicResult = Some.Random.TestRandomValue(opt);

                Check.That((object)dynamicResult).IsSameReferenceThan(opt);
            }
        }

        public class Use_named_func_method {
            [Fact]
            public void should_register_the_specified_function_with_the_classic_Some_api() {
                Generator.Use("TestValue", I => I);

                var opt = new {};

                var classicResult = Some.RandomValue<object>("TestValue", opt);

                Check.That(classicResult).IsSameReferenceThan(opt);
            }

            [Fact]
            public void should_register_the_specified_function_the_dynamic_Some_api() {
                Generator.Use("TestValue", I => I);

                var opt = new {};

                var dynamicResult = Some.Random.TestValue(opt);

                Check.That((object) dynamicResult).IsSameReferenceThan(opt);
            }
        }

        public class Use_func_method {
            [Fact]
            public void should_return_its_func_argument() {
                var func = new Func<object, object>(x => x);
                var actual = Generator.Use(func);

                Check.That(actual).IsSameReferenceThan(func);
            }
        }
    }



    public class Person {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
