namespace FakerOfData.TextDestination {
    static class Extensions {
        public static string ToNullString(this object self, string nullValue = "<null>") {
            return (self ?? nullValue).ToString();
        }
    }
}