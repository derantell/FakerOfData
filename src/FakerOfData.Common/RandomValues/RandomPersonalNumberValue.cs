﻿using System;
using System.Linq;

namespace FakerOfData.Common.RandomValues {

    public enum PersonalNumberFormat { Full, Short, Friendly }
    public enum Sex                  { Any, Male, Female }

    public class RandomPersonalNumberValue : IRandomValue {
        public Func<object, object> RandomValues {
            get { return RandomValue; }
        }

        public string Key {
            get { return "PersonalNumber"; }
        }

        
        private static object RandomValue( object options ) {
            var from   = options.Get("from", 110.Years().Ago());
            var to     = options.Get("to", DateTime.Now);
            var sex    = options.Get<Sex>("sex");
            var format = options.Get<PersonalNumberFormat>("format");

            var birthDate   = Generator.Random.Date(new {from,to});
            var datePart    = birthDate.ToString("yyMMdd");
            var birthNumber = GetBirthNumber( Generator.Random.Next(1000), sex );
            var first9      = datePart + birthNumber;

            var count = 0;
            var sumOfProducts = first9.Aggregate(0, (s, c) => {
                var factor  = count++%2 == 0 ? 2 : 1;
                var product = factor*(int)Char.GetNumericValue(c);
                return s + product/10 + product%10;
            });

            var checkdigit = (10 - (sumOfProducts%10))%10;

            return FormatPersonalNumber(birthDate, string.Format("{0}{1}", birthNumber, checkdigit), format);
        }

        private static string GetBirthNumber(int number, Sex sex) {
            switch (sex) {
                case Sex.Male:
                    return ((number/2*2+1)%1000).ToString("00#");
                case Sex.Female:
                    return ((number/2*2)%1000).ToString("00#");
                default:
                    return number.ToString("00#");
            }
        }

        private static string FormatPersonalNumber(DateTime birthDate, string last4, PersonalNumberFormat format) {
            switch (format) {
                case PersonalNumberFormat.Full:
                    return string.Format("{0:yyyyMMdd}{1}", birthDate, last4);
                case PersonalNumberFormat.Short:
                    return string.Format("{0:yyMMdd}{1}", birthDate, last4);
                case PersonalNumberFormat.Friendly:
                    var sign = (DateTime.Now.Year - birthDate.Year > + 100) ? "+" : "-";
                    return string.Format("{0:yyMMdd}{1}{2}", birthDate, sign, last4);
                default:
                    throw new ArgumentOutOfRangeException("format");
            }
        }
    }
}