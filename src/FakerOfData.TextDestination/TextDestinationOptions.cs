namespace FakerOfData.TextDestination {
    public struct TextDestinationOptions {
        public TextDestinationOptions(
            string fieldSeparator = "\t",
            string lineSeparator = "\r\n",
            bool firstLineIsHeaders = false,
            bool splitHeaderText = false,
            string[] fieldOrder = null,
            bool quoteFields = false,
            string quoteCharacter = "\""
            ) {
            FirstLineIsHeaders = firstLineIsHeaders;
            SplitHeaderText = splitHeaderText;
            FieldSeparator = fieldSeparator;
            LineSeparator = lineSeparator;
            FieldOrder = fieldOrder ?? new string[0];
            QuoteCharacter = quoteCharacter;
            QuoteFields = quoteFields;
        }

        public readonly bool FirstLineIsHeaders;
        public readonly bool SplitHeaderText;
        public readonly string FieldSeparator;
        public readonly string LineSeparator;
        public readonly string[] FieldOrder;
        public readonly bool QuoteFields;
        public readonly string QuoteCharacter;

        public static readonly TextDestinationOptions Default = new TextDestinationOptions();
    }
}