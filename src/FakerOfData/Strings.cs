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

        public static string MaleName(this Random self) {
            return self.String("men");
        }

        public static string FemaleName(this Random self) {
            return self.String("women");
        }

        public static string LastName(this Random self) {
            return self.String("lastnames");
        }

        public static string FirstName(this Random self) {
            return self.NextDouble() > .5 ? self.MaleName() : self.FemaleName();
        }

        public static string FullName(this Random self) {
            return self.FirstName() + " " + self.LastName();
        }
    }
}