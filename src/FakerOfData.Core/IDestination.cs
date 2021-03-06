﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace FakerOfData {
    public interface IDestination {
        IEnumerable<T> Load<T>(IEnumerable<T> sequence);
    }
}