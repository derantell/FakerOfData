using System.Collections.Generic;

namespace FakerOfData {
    public interface IDestination {
        IEnumerable<T> Load<T>(IEnumerable<T> sequence);
    }
}