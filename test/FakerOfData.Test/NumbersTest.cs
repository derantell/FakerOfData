using System;
using FakeItEasy;
using NFluent;
using Xunit;

namespace FakerOfData.Test {
    public class NumbersTest {

        public class PersonalNumber_method {

            [Fact]
            public void should_generate_a_random_valid_swedish_personal_number() {
                var random = new Random();

                var persnr = random.PersonalNumber();

                Check.That(persnr).HasSize(12);
            }

            [Fact]
            public void should_generate_a_number_within_the_specified_date_range() {
                var random = new Random();

                var persnr = random.PersonalNumber( from: new DateTime(1976,1,1), to: new DateTime(1976,1,1,23,59,59) );

                Check.That(persnr).StartsWith("19760101");
            }

            [Fact]
            public void should_generate_a_full_number_when_full_flag_is_set() {
                var random = new Random();

                var persnr = random.PersonalNumber(format: PersonalNumberFormat.Full);

                Check.That(persnr).Matches(@"\d{12}");
            }

            [Fact]
            public void should_generate_a_short_number_when_short_flag_is_set() {
                var random = new Random();

                var persnr = random.PersonalNumber(format: PersonalNumberFormat.Short);

                Check.That(persnr).Matches(@"\d{10}");
            }

            [Fact]
            public void should_generate_a_friendly_number_when_short_flag_is_set() {
                var random = new Random();

                var persnr = random.PersonalNumber(format: PersonalNumberFormat.Friendly);

                Check.That(persnr).Matches(@"\d{6}[-+]\d{4}");
            }

            [Fact]
            public void should_generate_a_friendly_number_with_a_plus_sign_if_person_is_over_100() {
                var random = new Random();

                var persnr = random.PersonalNumber(
                    from: new DateTime(1902,1,1),
                    to:   new DateTime(1911,12,31),
                    format: PersonalNumberFormat.Friendly);

                Check.That(persnr).Matches(@"\d{6}[+]\d{4}");
            }

            [Fact]
            public void should_generate_a_number_with_an_even_birthnumber_when_sex_flag_is_female() {
                var random = new Random();

                var persnr = random.PersonalNumber(sex: Sex.Female);

                Check.That(persnr).Matches(@"\d{10}[24680]\d");
            }

            [Fact]
            public void should_generate_a_number_with_an_odd_birthnumber_when_sex_flag_is_male() {
                var random = new Random();

                var persnr = random.PersonalNumber(sex: Sex.Male);

                Check.That(persnr).Matches(@"\d{10}[13579]\d");
            }
        }
    }
}
