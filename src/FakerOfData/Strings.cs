using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FakerOfData {
    public static class Strings {
        public static string String(this Random self, string key) {
            IEnumerable<string> strings = Generator.Strings[key];
            return strings.ElementAt(self.Next(strings.Count()));
        }
    }
}