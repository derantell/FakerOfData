using System;

namespace FakerOfData {
    public interface IRandomValue {
        Func<object, object> RandomValues { get; }
        string Key { get; }
    }
}