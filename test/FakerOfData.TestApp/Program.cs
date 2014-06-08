using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace FakerOfData.TestApp {
    class TheTestData {
        public int Index { get; set; }
        public string TimeStamp { get; set; }
        public Guid UniqueId { get; set; }
        public string Category { get; set; }
    }

    static class Ext {
        public static string Category(this Random self) {
            return self.String("Category");
        }
    }

    class Program {
        static void Main(string[] args) {
            Generator.Destination = new CsvDestination("\t", s => new StreamWriter(@"c:\slask\" + s));
            Generator.Strings = new DynamicStringCollection(new FileStringSource("Strings"));

            Generator.Of<TheTestData>(
                d => d.Index = Counter.Next.Index,
                d => d.TimeStamp = Some.Random.DateBetween(2.Years().Ago(), 1.Years().FromNow()).ToString("s"),
                d => d.UniqueId = Guid.NewGuid(),
                d => d.Category = Some.Random.Category())

                .Take(1000)
                .Load();
        }
    }
}
