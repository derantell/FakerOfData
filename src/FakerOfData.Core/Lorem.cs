using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FakerOfData {
    public static class Lorem {

        public static string Ipsum(int wordCount = 5, bool startWithLoremIpsum = false) {
            var loremIpsum = startWithLoremIpsum ? "Lorem ipsum dolor sit amet. " : "";
            var skip = startWithLoremIpsum ? 5 : 0;
            loremIpsum = loremIpsum + Sentenceify(Words.Take(wordCount - skip));

            return loremIpsum.Trim();
        }

        private static string Sentenceify(IEnumerable<string> words) {
            var text = new StringBuilder();
            var wordCount = words.Count();
            var handledWords = 0;

            while (handledWords < wordCount) {
                var length = RandGen.Next(5, 10);
                var sentence = string.Join(" ", words.Skip(handledWords).Take(length));
                sentence = Regex.Replace(sentence, @"^\w", match => match.Value.ToUpper());
                text.AppendFormat("{0}. ", sentence);
                handledWords += length;
            }

            return text.ToString();
        }

        private static IEnumerable<string> Words {
            get {
                while (true) {
                    yield return WordList[RandGen.Next(WordList.Length - 1)];
                }
            }
        }

        private static readonly Random RandGen = new Random();

        private static readonly string[] WordList = {
            "lorem",        "ipsum",      "dolor",       "sit",         "amet",
            "consectetur",  "adipiscing", "elit",        "vestibulum",  "sagittis",
            "ac",           "sapien",     "ullamcorper", "in",          "tincidunt",
            "lectus",       "vehicula",   "imperdiet",   "velit",       "congue",
            "semper",       "etiam",      "ut",          "sem",         "aliquam",
            "commodo",      "dui",        "vitae",       "lobortis",    "est",
            "fusce",        "iaculis",    "bibendum",    "convallis",   "pulvinar",
            "arcu",         "quisque",    "mattis",      "augue",       "a",
            "lacinia",      "diam",       "euismod",     "praesent",    "eu",
            "libero",       "justo",      "cras",        "dapibus",     "nulla",
            "nec",          "suscipit",   "sed",         "vel",         "urna",
            "risus",        "duis",       "aliquet",     "consequat",   "massa",
            "at",           "proin",      "placerat",    "condimentum", "rhoncus",
            "pellentesque", "quis",       "scelerisque", "curabitur",   "varius",
            "purus",        "eget",       "hendrerit",   "fringilla",   "ligula",
            "tempus",       "magna",      "tortor",      "faucibus",    "malesuada",
            "nisl",         "vivamus",    "orci",        "cursus",      "eleifend",
            "felis",        "mollis",     "non",         "posuere",     "neque",
            "nibh",         "mauris",     "erat",        "turpis",      "tempor",
            "id",           "odio",       "porta",       "suspendisse", "metus",
            "laoreet",      "blandit",    "mi",          "ultricies",   "ornare",
            "sodales",      "dignissim",  "interdum",    "facilisis",   "volutpat",
            "vulputate",    "et",         "nam",         "venenatis",   "integer",
            "nunc",         "rutrum",     "pharetra",    "donec",       "accumsan",
            "ante",         "nullam",     "maecenas",    "quam",        "luctus",
            "aenean",       "egestas",    "nisi",        "feugiat",     "tellus",
            "enim",         "fermentum" 
        };
    }
}