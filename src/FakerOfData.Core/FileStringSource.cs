using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public IEnumerable<string> Keys {
            get { 
                return _keys ?? (
                    _keys = Directory
                        .GetFiles(_directory, "*.txt")
                        .Select(Path.GetFileNameWithoutExtension)
                        .ToArray()
                ); 
            }
        }

        private string[] _keys;
        private readonly string _directory;
    }
}