using System;
using System.Collections.Generic;
using System.Linq;

namespace FakerOfData {
    public static class Some {
        public static Random Random {
            get { return _random; }
        }

        public static T ItemFrom<T>(this Random self, IEnumerable<T> sequence) {
            var index = self.Next(sequence.Count());
            return sequence.ElementAt(index);
        }

    private static readonly Random _random = new Random();
    }
}