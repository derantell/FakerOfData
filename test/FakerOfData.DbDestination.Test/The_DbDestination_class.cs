using System.Collections.Generic;
using System.Linq;
using NFluent;
using Simple.Data;
using Xunit;

namespace FakerOfData.DbDestination.Test {
    public class The_DbDestination_class {

        public class Load_method {

            [Fact]
            public void should_insert_the_specified_collection_into_the_db() {
                var adapter = new InMemoryAdapter();
                Database.UseMockAdapter(adapter);

                var destination = new DbDestination("foo");

                destination.Load(new[] {
                    new Record {Id = 42, Name = "Foo"},
                    new Record {Id = 1337, Name = "Bar"}
                });

                var records = Database.Open().Record.All().ToList();

                Assert.Equal(records.Count, 2);
            }

            [Fact]
            public void should_return_the_collection_with_ids_set_if_table_has_identity_col() {
                var adapter = new InMemoryAdapter();
                adapter.SetAutoIncrementColumn("Record", "Id");
                Database.UseMockAdapter(adapter);

                var destination = new DbDestination("foo");

                var inserted = destination.Load(new[] {
                    new Record {Name = "Foo"},
                    new Record {Name = "Bar"}
                });

                Assert.True(inserted.First().Id > 0);
            }
        }

        public class Record {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
