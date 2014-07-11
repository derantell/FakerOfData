using System.Collections.Generic;
using System.Linq;

namespace FakerOfData {
    public static class Draw {
        public static object One(params object[] items) {
            return items[Generator.Random.Next(items.Length)];
        }

        public static T DrawRandomItem<T>(this IEnumerable<T> self) {
            var index = Generator.Random.Next(self.Count());
            return self.ElementAt(index);
        }
    }
}