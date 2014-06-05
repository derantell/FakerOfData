using System;
using System.Linq;
using NFluent;
using Xunit;

namespace FakerOfData.Test {
    public class GeneratorTest {

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
    }



    public class Person {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
