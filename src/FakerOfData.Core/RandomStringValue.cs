using System;
using System.Collections.Generic;

namespace FakerOfData {
    public class RandomStringValue : IRandomValue {
        public RandomStringValue(string key, IStringSource source) {
            _source = source;
            Key = key;
        }

        public Func<object, object> RandomValues {
            get {
                return _ => (_strings ?? (_strings = _source.GetStrings(Key))).DrawRandomItem();
            }
        }
        public string Key { get; private set; }

        private IEnumerable<string> _strings;
        private readonly IStringSource _source;
    }
}