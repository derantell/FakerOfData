using System;
using NFluent;
using Xunit;

namespace FakerOfData.Test {
    public class DatesAndTimesTest {

        public class Date_method {

            [Fact]
            public void should_return_a_random_datetime_object() {
                var random = new Random();

                var date1 = random.Date();
                var date2 = random.Date();

                Check.That(date1).Not.IsEqualTo(date2);
            }

            [Fact]
            public void should_return_a_date_between_the_two_specified_dates() {
                var random = new Random();

                var from = new DateTime(1976, 1, 2);
                var to = new DateTime(1976, 1, 3);

                var theDate = random.Date(new{from, to});

                Check.That(theDate).IsAfter(from).And.IsBefore(to);
            }
        }

        public class Years_method {

            [Fact]
            public void should_return_function_that_subtracts_years_from_specified_date_when_direction_is_past() {
                var years = 2.Years();
                var date = new DateTime(1976, 1, 1);
                var twoYearsPast = years(date, TimeDirection.Past);
                Check.That(twoYearsPast.Year).IsEqualTo(1974);
            }

            [Fact]
            public void should_return_a_function_that_adds_the_years_to_specified_date_when_direction_is_future() {
                var years = 2.Years();
                var date = new DateTime(1976, 1, 1);
                var twoYearsFuture = years(date, TimeDirection.Future);
                Check.That(twoYearsFuture.Year).IsEqualTo(1978);
            }
        }

        public class Months_method{
            [Fact]
            public void should_return_a_function_that_subtracts_months_from_specified_date_when_direction_is_past() {
                var months = 2.Months();
                var date = new DateTime(1976, 1, 1);
                var twoMonthsPast = months(date, TimeDirection.Past);
                Check.That(twoMonthsPast.Month).IsEqualTo(11);
            }

            [Fact]
            public void should_return_a_function_that_adds_months_from_specified_date_when_direction_is_past() {
                var months = 2.Months();
                var date = new DateTime(1976, 1, 1);
                var twoMonthsFuture = months(date, TimeDirection.Future);
                Check.That(twoMonthsFuture.Month).IsEqualTo(3);
            }
        }

        public class Days_method {
            [Fact]
            public void should_return_a_function_that_subtracts_days_from_specified_date_when_direction_is_past() {
                var days = 2.Days();
                var date = new DateTime(1976, 1, 1);
                var twoDaysPast = days(date, TimeDirection.Past);
                Check.That(twoDaysPast.Day).IsEqualTo(30);
            }

            [Fact]
            public void should_return_a_function_that_adds_days_from_specified_date_when_direction_is_past() {
                var days = 2.Days();
                var date = new DateTime(1976, 1, 1);
                var twoDaysFuture = days(date, TimeDirection.Future);
                Check.That(twoDaysFuture.Day).IsEqualTo(3);
            }
        }

        public class Ago_method {
            [Fact]
            public void should_subract_time_from_a_date() {
                var actualDirection = TimeDirection.Future;
                Func<DateTime, int, DateTime> function =
                    (d, t) => { actualDirection = t; return d; };

                function.Ago();

                Check.That(actualDirection).IsEqualTo(TimeDirection.Past);
            }
        }

        public class FromNow_method {
            [Fact]
            public void should_add_time_to_a_date() {
                var actualDirection = TimeDirection.Past;
                Func<DateTime, int, DateTime> function =
                    (d, t) => { actualDirection = t; return d; };

                function.FromNow();

                Check.That(actualDirection).IsEqualTo(TimeDirection.Future);
            }
        }

        public class Before_method {
            [Fact]
            public void should_subtract_time_from_the_specified_date() {
                var actualDirection = TimeDirection.Future;
                var referenceDate = new DateTime(1976, 1, 1);
                
                Func<DateTime, int, DateTime> function =
                    (d, t) => { actualDirection = t; return d; };

                var actualDate = function.Before(referenceDate);

                Check.That(actualDirection).IsEqualTo(TimeDirection.Past);
                Check.That(actualDate).IsEqualTo(referenceDate);
            }
        }

        public class After_method {
            [Fact]
            public void should_subtract_time_from_the_specified_date() {
                var actualDirection = TimeDirection.Past;
                var referenceDate = new DateTime(1976, 1, 1);

                Func<DateTime, int, DateTime> function =
                    (d, t) => { actualDirection = t; return d; };

                var actualDate = function.After(referenceDate);

                Check.That(actualDirection).IsEqualTo(TimeDirection.Future);
                Check.That(actualDate).IsEqualTo(referenceDate);
            }
        }

        public class Integration_tests {
            [Fact]
            public void years_ago() {
                var sixYearsAgo = 6.Years().Ago();

                Check.That(sixYearsAgo.Year).IsEqualTo(DateTime.Today.Year - 6);
            }

            [Fact]
            public void months_from_now() {
                var threeMonthsFromNow = 3.Months().FromNow();
                Check.That(threeMonthsFromNow.Month).IsEqualTo(DateTime.Today.AddMonths(3).Month);
            }

            [Fact]
            public void six_days_before_christmas_day_1985() {
                var day = 6.Days().Before(new DateTime(1985, 12, 25));
                Check.That(day.Day).IsEqualTo(19);
            }

            [Fact]
            public void the_day_after_tomorrow() {
                var theDay = 1.Days().After(DateTime.Today.AddDays(1));
                Check.That(theDay - DateTime.Today).Equals(TimeSpan.FromDays(2));
            }
        }
    }
}
