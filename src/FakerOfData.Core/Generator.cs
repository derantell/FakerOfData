using System;
using System.Collections.Generic;
using System.Linq;

namespace FakerOfData {
    public static class Generator {
        public static IEnumerable<T> Of<T>(params Action<T>[] setters) where T : new() {
            while (true) { yield return setters.Aggregate(new T(), (i, a) => { a(i); return i; }); }
        }

        public static IDestination Destination { get; set; }
        public static Random Random { get; private set; }

        public static void Use<TRandomValue>() where TRandomValue : IRandomValue, new() {
            Some.RandomThing(new TRandomValue());  
        }

        public static void Use(string valueName, Func<object, object> values) {
            Some.RandomThing(valueName, values);
        }

        public static Func<object, object> Use(Func<object, object> values) {
            return values;
        } 

        static Generator() {
            Random = new Random();

            Some.RandomThings( new RandomDateValue() );
            Some.RandomThings( new FileStringSource("Strings").GetRandomValues() );
        }
    }
}