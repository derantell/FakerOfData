using System.Collections.Generic;

namespace FakerOfData {
    public interface IStringSource {
        IEnumerable<string> Keys { get; }
        IEnumerable<string> GetStrings(string key);
    }
}