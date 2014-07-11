﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace FakerOfData.Common.RandomValues {
    public class RandomStringPatternValue : IRandomValue {
        public string Key { get { return "StringPattern"; }}

        public Func<object, object> RandomValues {
            get { return opt => {
                var pattern = opt.Get<string>("pattern");

                if (pattern == null) return "";

                var parser = new PatternParser();
                var patternParts = parser.Parse(pattern);

                return patternParts.Generate();
            }; }
        }

        class PatternParser {
            public StringPattern Parse(string pattern) {
                var result = new StringPattern();
                var parts = Pattern.Match(pattern);
                var currentMatch = parts;

                do {
                    var part = HandlePart(currentMatch);
                    result.Add(part);
                    currentMatch = currentMatch.NextMatch();
                } while (currentMatch.Success);

                return result;
            }

            private PatternPart HandlePart(Match match) {
                Func<string> generator = () => "";
                if (match.Groups["static"].Success) {
                    generator = () => match.Groups["static"].Value;
                }

                if (match.Groups["cclass"].Success) {
                    var chars = match.Groups["cclass"].Value;
                    var min = match.Groups["min"].Success ? int.Parse(match.Groups["min"].Value) : 1;
                    var max = match.Groups["max"].Success ? int.Parse(match.Groups["max"].Value) : min;
                    var charClass = new CharacterClass(chars, min, max);
                    generator = charClass.Next;
                }

                return new PatternPart(generator);
            }

            private static readonly Regex Pattern = new Regex(
                @" # 
                \[(?<cclass>.+?)\](?:\{(?<min>\d+)(?:,(?<max>\d+))?\})?
                |
                (?<static>[^\[{]+)
                "
                , RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace
            );
        }

        class CharacterClass {
            public CharacterClass(string alphabet, int min = 1, int max = 1) {
                _alphabet = alphabet;
                _min = min;
                _max = max;
            }

            public string Next() {
                var length = Random.Next(_min, _max + 1);
                var chars = new char[length];

                for (int i = 0; i < length; i++) {
                    chars[i] = _alphabet[Random.Next(_alphabet.Length)];
                }
                return new string(chars);
            }

            private readonly string _alphabet;
            private readonly int _min;
            private readonly int _max;
            private static readonly Random Random = new Random();
        }

        class StringPattern {
            public void Add(PatternPart part) {
                parts.Add(part);
            }

            public string Generate() {
                var generatedParts = parts.Select( pp => pp.Generate());
                return string.Join("", generatedParts);
            }

            private readonly List<PatternPart> parts = new List<PatternPart>();
        }

        class PatternPart {
            public PatternPart(Func<string> generator) {
                _generator = generator;
            }

            public string Generate() {
                return _generator();
            }

            private readonly Func<string> _generator;
        }
    }
}