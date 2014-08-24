using System.Collections.Generic;

namespace FakerOfData {
    public static class EnumerableExtensions {
        public static IEnumerable<T> Load<T>(this IEnumerable<T> self) {
            return Generator.Destination.Load(self);
        }
    }
}