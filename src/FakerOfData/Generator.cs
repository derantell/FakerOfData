using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FakerOfData {
    public static class Generator {
        public static IEnumerable<T> Of<T>(params Action<T>[] setters) where T : new() {
            while (true) { yield return setters.Aggregate(new T(), (i, a) => { a(i); return i; }); }
        }

        public static IDestination Destination { get; set; }
    }
}