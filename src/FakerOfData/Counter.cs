using System.Collections.Generic;
using System.Dynamic;

namespace FakerOfData {
    public static class Counter {
        public static dynamic Next {
            get { return _properties; }
        }

        public static int NextFor(string key) {
            return _properties[key];
        }

        public static void SetFor(string key, int value) {
            _properties[key] = value;
        } 

        private static readonly AutoProperty _properties = new AutoProperty();

        private class AutoProperty : DynamicObject {
            private readonly Dictionary<string, int> _counters = new Dictionary<string, int>();

            public int this[string key] {
                get {
                    if (!_counters.ContainsKey(key)) {
                        _counters[key] = 0;
                    }

                    _counters[key] += 1;
                    return _counters[key];
                }

                set { _counters[key] = value; }
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result) {
                if (!_counters.ContainsKey(binder.Name)) {
                    _counters[binder.Name] = 0;
                }

                _counters[binder.Name] += 1;
                result = _counters[binder.Name];
                return true;
            }

            public override bool TrySetMember(SetMemberBinder binder, object value) {
                _counters[binder.Name] = (int)value;
                return true;
            }
        }
    }
}