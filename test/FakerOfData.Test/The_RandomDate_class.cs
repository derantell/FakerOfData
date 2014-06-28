using System;
using FakeItEasy;
using NFluent;
using Xunit;

namespace FakerOfData.Test {
    public class The_RandomDate_class {

        public class Key_property {

            [Fact]
            public void should_always_return_the_value_Date() {
                var randomdate = new RandomDateValue();
                Check.That(randomdate.Key).IsEqualTo("Date");
            }

        }

        public class RandomValues_function_property {
            [Fact]
            public void should_return_a_random_date_between_one_year_ago_and_today_when_called_without_options(){
                var randomDate = new RandomDateValue();

                var date = (DateTime)randomDate.RandomValues(new{});

                var now = DateTime.Now;

                Check.That(date).IsAfter(now.AddYears(-1)).And.IsBefore(now);
            }

            [Fact]
            public void should_return_a_random_date_within_specified_from_and_to_limits() {
                var randomDate = new RandomDateValue();

                var from = new DateTime(1983, 4, 9);
                var to   = new DateTime(1984, 11, 3);

                var date = (DateTime) randomDate.RandomValues(new {from, to});
                Check.That(date).IsAfter(from).And.IsBefore(to);
            }

            [Fact]
            public void should_return_a_string_representing_random_date_using_specified_datetime_format() {
                var randomDate = new RandomDateValue();

                var format = "yyyy-MM-dd";

                var date = (string) randomDate.RandomValues(new {format});

                Check.That(date).Matches(@"\d{4}-\d{2}-\d{2}");
            }
        }
    }
}
