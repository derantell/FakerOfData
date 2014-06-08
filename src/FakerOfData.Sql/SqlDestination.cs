using System.Collections.Generic;
using Simple.Data;

namespace FakerOfData.Sql {
    public class SqlDestination : IDestination {
        public SqlDestination(string connectionString) {
            _connectionString = connectionString;
            _db = Database.OpenConnection(_connectionString);
        }

        public void Load<T>(IEnumerable<T> sequence) {
            var tableName = typeof (T).Name;
            _db[tableName].Insert(sequence);
        }

        private readonly dynamic _db;
        private readonly string _connectionString;
    }
}