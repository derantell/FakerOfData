using System.Collections.Generic;
using System.Dynamic;

namespace FakerOfData {
    public class DynamicStringCollection : DynamicObject {
        public DynamicStringCollection(IStringSource stringSource) {
            _stringSource = stringSource;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result) {
            IEnumerable<string> stringCollection;
            if (_strings.TryGetValue(binder.Name, out stringCollection)) {
                result = stringCollection;
                return true;
            }

            result = _strings[binder.Name] = _stringSource.GetStrings(binder.Name);
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value) {
            _strings[binder.Name] = (IEnumerable<string>) value;
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result) {
            IEnumerable<string> stringCollection;
            var key = (string) indexes[0];
            if (_strings.TryGetValue(key, out stringCollection)) {
                result = stringCollection;
                return true;
            }

            result = _strings[key] = _stringSource.GetStrings(key);
            return true;
        }

        private readonly IStringSource _stringSource;
        private readonly Dictionary<string, IEnumerable<string>> _strings =
            new Dictionary<string, IEnumerable<string>>();
    }

    public interface IStringSource {
        IEnumerable<string> GetStrings(string key);
    }
}