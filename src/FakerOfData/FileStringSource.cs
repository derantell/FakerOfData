using System.Collections.Generic;
using System.IO;

namespace FakerOfData {
    public class FileStringSource : IStringSource {
        public FileStringSource(string directory) {
            _directory = directory;
        }

        public IEnumerable<string> GetStrings(string key) {
            var filePath = Path.Combine(_directory, key + ".txt");
            var lines = File.ReadAllLines(filePath);
            return lines;
        }

        private readonly string _directory;
    }
}