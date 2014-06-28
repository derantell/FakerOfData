using System;
using System.IO;
using System.Linq;
using System.Threading;
using FakerOfData;
using FakerOfData.Csv;

namespace Foo.FakerOfData.TestApp {
    class TheTestData {
        public int Index { get; set; }
        public string TimeStamp { get; set; }
        public Guid UniqueId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string PersonalNumber { get; set; }
        public string Text { get; set; }
    }

    class FavouriteCandy : IRandomValue {
        public Func<object, object> RandomValues {
            get {
                return opt => {
                    var kind = opt.Get<CandyKind>("kind");
                    var candies = kind == CandyKind.Fruity
                        ? new[] {"Gott och blandat", "Moaom", "Aco fruktkola"}
                        : new[] {"Salta katten", "Lakritsal", "Turkisk peppar"};
                    return Draw.One(candies);
                };
            }
        }

        public string Key { get { return "FavouriteCandy"; }}
    }

    public enum CandyKind { Salty, Fruity}

    class Program {
        static void Main(string[] args) {
            //Generator.Destination = new CsvDestination("\t", s => new StreamWriter(@"c:\slask\" + s));
            Generator.Destination = new CsvDestination(",", s => Console.Out);
            Generator.Use("Category", _ => Draw.One("Foo", "Bar", "Baz"));
            Generator.Require<FavouriteCandy>();

            Generator
                .Of<TheTestData>(
                    d => d.Index     = Counter.Next.Brax,
                    d => d.TimeStamp = Some.Random.Date(new {from=2.Years().Ago(), to= 1.Years().FromNow(), format="yyMM--dd"}),
                    d => d.UniqueId  = Guid.NewGuid(),
                    d => d.Text      = Lorem.Ipsum(10),
                    d => d.PersonalNumber = Some.Random.PersonalNumber,
                    d => d.Category  = Some.Random.FavouriteCandy(new {kind = CandyKind.Fruity}))
                .Take(5)
                .Load();

            Some.RandomThing( "Foo", d => "Hejsan");
            var foo = Some.Random.Foo;
            Console.WriteLine("This is foo: " + foo);

            var braz = Some.Random.lastnames;
            Console.WriteLine("name: " + braz);

            var newLastName = Some.RandomValue<string>("Date", 
                new {from=DateTime.Now, to = 2.Years().FromNow(), format="yyyyy-MM"}
            );
            Console.WriteLine("name: " + newLastName);

            Some.Random.Braxen = new Func<object,object>(_ => "Allan");
            Console.WriteLine("Allan? " + Some.Random.Braxen);

            Some.RandomThing("Name", _ => Draw.One(Some.Random.men, Some.Random.women) );
            Some.RandomThing("FullName", _ =>  Some.Random.Name + " " + Some.Random.lastnames);
            Console.WriteLine("name: " + Some.Random.FullName);

            Some.RandomThing("Ball", _ => Draw.One("Blue", "Green"));
            Console.WriteLine( Some.Random.Ball );

            Console.WriteLine( Some.Random.Date( new { from = 1.Years().Ago(), to = 2.Months().Ago() }));
        }
    }
}
