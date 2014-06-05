using System;

namespace FakerOfData {
    public static class Some {
        public static Random Random {
            get { return _random; }
        }

        private static readonly Random _random = new Random();
    }
}