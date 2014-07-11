using System;
using System.Collections.Generic;
using System.Linq;

namespace FakerOfData {
    public static class Some {
        public static dynamic Random {
            get { return _randomThings; }
        }

        public static void RandomThing(string key, Func<object, object> values) {
            _randomThings.RegisterValues(key, values);
        }

        public static void RandomThing(IRandomValue thing) {
            _randomThings.RegisterValues(thing.Key, thing.RandomValues);
        }

        public static void RandomThings(params IRandomValue[] things) {
            RandomThings( things.AsEnumerable() );
        }

        public static void RandomThings(IEnumerable<IRandomValue> things) {
            foreach (var randomValue in things) {
                _randomThings.RegisterValues(randomValue.Key, randomValue.RandomValues);
            }
        }

        public static T RandomValue<T>(string key, object options = null) {
            var values = (Dictionary<string, Func<object, object>>) _randomThings;
            return (T) values[key](options);
        }

        // Clears the RandomThings collection, only used for unit testing.
        internal static void Clear() {
            _randomThings = new RandomThings();
        }

        private static RandomThings _randomThings = new RandomThings();

    }
}