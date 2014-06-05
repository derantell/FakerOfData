using System.Collections.Generic;

namespace FakerOfData {
    public static class EnumerableExtensions {
        public static void Load<T>(this IEnumerable<T> self) {
            Generator.Destination.Load(self);
        }
    }
}