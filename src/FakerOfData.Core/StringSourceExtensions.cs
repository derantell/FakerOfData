using System.Collections.Generic;
using System.Linq;

namespace FakerOfData {
    public static class StringSourceExtensions {
        public static IEnumerable<IRandomValue> GetRandomValues(this IStringSource self) {
            return self.Keys.Select(key => new RandomStringValue(key, self));
        }
    }
}