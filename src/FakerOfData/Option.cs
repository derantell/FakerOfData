
using System.Reflection;

namespace FakerOfData {
    public static class Option {
        public static TOption Get<TOption>(this object self, string name, TOption @default = default(TOption)) {
            if (self == null) return @default;

            const BindingFlags bindingFlags = 
                BindingFlags.IgnoreCase | 
                BindingFlags.Public     | 
                BindingFlags.Instance;

            var property = self.GetType().GetProperty(name, bindingFlags);

            if (property == null) return @default;
            return (TOption) property.GetValue(self);
        }
    }
}