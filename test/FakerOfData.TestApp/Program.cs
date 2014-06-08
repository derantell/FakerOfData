using System;
using System.IO;
using System.Linq;
using FakerOfData.Csv;

namespace FakerOfData.TestApp {
    class TheTestData {
        public int Index { get; set; }
        public string TimeStamp { get; set; }
        public Guid UniqueId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string PersonalNumber { get; set; }
        public string Text { get; set; }
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
                d => d.Index     = Counter.Next.Index,
                d => d.TimeStamp = Some.Random.DateBetween(2.Years().Ago(), 1.Years().FromNow()).ToString("s"),
                d => d.UniqueId  = Guid.NewGuid(),
                d => d.Text      = Lorem.Ipsum(10),
                d => d.PersonalNumber = Some.Random.PersonalNumber(format:PersonalNumberFormat.Friendly),
                d => d.Name      = Some.Random.FullName(),
                d => d.Category  = Some.Random.Category())

                .Take(1000)
                .Load();
        }
    }
}
