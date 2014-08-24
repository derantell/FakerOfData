using System.Collections.Generic;
using Simple.Data;

namespace FakerOfData.DbDestination {
    public class DbDestination : IDestination {
        public DbDestination(string connectionString) {
            _db = Database.OpenConnection(connectionString);
        }

        public IEnumerable<T> Load<T>(IEnumerable<T> sequence) {
            var tableName = typeof (T).Name;
            return _db[tableName].Insert(sequence);
        }

        private readonly dynamic _db;
    }
}