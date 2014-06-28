using System;
using System.Collections.Generic;
using System.Dynamic;

namespace FakerOfData {
    public class RandomThings : DynamicObject {
        private readonly Dictionary<string, Func<object, object>> _valueProviders =
            new Dictionary<string, Func<object, object>>();

        public override bool TryGetMember(GetMemberBinder binder, out object result) {
            result = GetValue(binder.Name, null);
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value) {
            _valueProviders[binder.Name] = (Func<object, object>) value;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result) {
            var arg = args.Length > 0 ? args[0] : null;
            var key = binder.Name;

            result = GetValue(key, arg);
            return true;
        }

        private object GetValue(string key, object arg) {
            Func<object, object> provider;

            if (_valueProviders.TryGetValue(key, out provider)) {
                return provider(arg);
            }
            
            throw new KeyNotFoundException("No random value provider specified for " + key);
        }

        public void RegisterValues(string name, Func<object, object> provider) {
            _valueProviders[name] = provider;
        }

        public static explicit operator Dictionary<string, Func<object, object>>(RandomThings things) {
            return things._valueProviders;
        }
    }
}