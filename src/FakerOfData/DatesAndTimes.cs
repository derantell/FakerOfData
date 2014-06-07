using System;
using System.Threading;

namespace FakerOfData {
    public static class DatesAndTimes {
        public static DateTime Date(this Random self) {
            return new DateTime( (long) (self.NextDouble() * DateTime.MaxValue.Ticks));
        }

        public static DateTime DateBetween(this Random self, DateTime? from = null, DateTime? to = null) {
            from = from ?? DateTime.MinValue;
            to   = to   ?? DateTime.MaxValue;

            var span = to.Value.Ticks - from.Value.Ticks;
            return new DateTime(from.Value.Ticks + (long)(span * self.NextDouble()));
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