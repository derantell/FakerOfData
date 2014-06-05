using System.Collections.Generic;
using System.Dynamic;

namespace FakerOfData {
    public static class Counter {
        public static dynamic Next {
            get { return _properties; }
        }

        private static readonly AutoProperty _properties = new AutoProperty();

        private class AutoProperty : DynamicObject {
            private readonly Dictionary<string, int> _counters = new Dictionary<string, int>();

            public override bool TryGetMember(GetMemberBinder binder, out object result) {
                int value;
                if (_counters.TryGetValue(binder.Name, out value)) {
                    result = value;
                    _counters[binder.Name] += 1;
                    return true;
                }

                result = _counters[binder.Name] = 1;
                return true;
            }

            public override bool TrySetMember(SetMemberBinder binder, object value) {
                _counters[binder.Name] = (int) value;
                return true;
            }
        }
    }
}