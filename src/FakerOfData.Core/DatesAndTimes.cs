using System;

namespace FakerOfData {
    public class RandomDateValue : IRandomValue {
        public Func<object, object> RandomValues {
            get {
                return options => {
                    var to     = options.Get("to", DateTime.Now);
                    var from   = options.Get("from", 1.Years().Ago());
                    var format = options.Get<string>("format");

                    var span = to.Ticks - from.Ticks;
                    var date = new DateTime(from.Ticks + (long) (span*Generator.Random.NextDouble()));

                    if (format != null) return date.ToString(format);

                    return date;
                };
            }
        }

        public string Key { get { return "Date"; }}
    }

    public static class DatesAndTimes {
        public static DateTime Date(this Random self, object options = null) {
            var to   = options.Get("to", DateTime.Now);
            var from = options.Get("from", 1.Years().Ago());

            var span = to.Ticks - from.Ticks;
            return new DateTime(from.Ticks + (long)(span * self.NextDouble()));
        }

        public static Func<DateTime, int, DateTime> Years(this int self) {
            return (d, t) => d.AddYears(t*self);
        }

        public static Func<DateTime, int, DateTime> Months(this int self) {
            return (d, t) => (t == TimeDirection.Past) ? d.AddMonths(-self) : d.AddMonths(self);
        }

        public static Func<DateTime, int, DateTime> Days(this int self) {
            return (d, t) => (t == TimeDirection.Past) ? d.AddDays(-self) : d.AddDays(self);
        }

        public static DateTime Ago(this Func<DateTime, int, DateTime> self) {
            return self(DateTime.Now, TimeDirection.Past);
        }

        public static DateTime FromNow(this Func<DateTime, int, DateTime> self) {
            return self(DateTime.Now, TimeDirection.Future);
        }

        public static DateTime Before(this Func<DateTime, int, DateTime> self, DateTime date) {
            return self(date, TimeDirection.Past);
        }

        public static DateTime After(this Func<DateTime, int, DateTime> self, DateTime date) {
            return self(date, TimeDirection.Future);
        }
    }

    public struct TimeDirection {
        public const int Past = -1;
        public const int Future = 1;
    }
}