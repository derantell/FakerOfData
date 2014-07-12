using System.Collections.Generic;
using Simple.Data;

namespace FakerOfData.Sql {
    public class SqlDestination : IDestination {
        public SqlDestination(string connectionString) {
            _db = Database.OpenConnection(connectionString);
        }

        public IEnumerable<T> Load<T>(IEnumerable<T> sequence) {
            var tableName = typeof (T).Name;
            return _db[tableName].Insert(sequence);
        }

        private readonly dynamic _db;
    }
}