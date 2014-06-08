using NFluent;
using Xunit;

namespace FakerOfData.Sql.Test {
    public class SqlDestinationTest {

        public class Load_method {

            [Fact]
            public void should_insert_the_specified_collection_into_the_db() {
                var destination = new SqlDestination("foo");

                destination.Load(new[]{new Record {Id = 42}});

                Check.That(destination);
            }

        }

        public class Record {
            public int Id { get; set; }
        }
    }
}
