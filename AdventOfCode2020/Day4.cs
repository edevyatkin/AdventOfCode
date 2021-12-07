using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day4
    {
        public static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("Day4_input");

            int valid = 0;
            User user = new User();
            foreach (string s in input)
            {
                if (s == string.Empty)
                {
                    if (ValidateUser(user))
                        valid++;
                    user.Clear();
                }
                else
                {
                    foreach (string sp in s.Split(' '))
                    {
                        var kv = sp.Split(':');
                        switch (kv[0])
                        {
                            case "byr":
                                user.BirthYear = kv[1];
                                break;
                            case "iyr":
                                user.IssueYear = kv[1];
                                break;
                            case "eyr":
                                user.ExpirationYear = kv[1];
                                break;
                            case "hgt":
                                user.Height = kv[1];
                                break;
                            case "hcl":
                                user.HairColor = kv[1];
                                break;
                            case "ecl":
                                user.EyeColor = kv[1];
                                break;
                            case "pid":
                                user.PassportId = kv[1];
                                break;
                            case "cid":
                                user.CountryId = kv[1];
                                break;
                        }
                    }

                }
            }
            Console.WriteLine("Day 4 " + valid);
        }

        private static bool ValidateUser(User user)
        {
            return ValidateDate(user.BirthYear, 1920, 2002) &&
                   ValidateDate(user.IssueYear, 2010, 2020) &&
                   ValidateDate(user.ExpirationYear, 2020, 2030) &&
                   ValidateHeight(user.Height) &&
                   ValidateHairColor(user.HairColor) &&
                   ValidateEyeColor(user.EyeColor) &&
                   ValidatePassportId(user.PassportId);
        }

        private static bool ValidatePassportId(string userPassportId)
        {
            return userPassportId != null && Regex.IsMatch(userPassportId, @"^\d{9}$");
        }

        private static bool ValidateEyeColor(string userEyeColor)
        {
            if (userEyeColor == null)
                return false;
            string[] colors = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
            return colors.Contains(userEyeColor);
        }

        private static bool ValidateHairColor(string userHairColor)
        {
            return userHairColor != null && Regex.IsMatch(userHairColor, @"#[0-9a-f]{6}");
        }

        private static bool ValidateHeight(string userHeight)
        {
            if (userHeight == null)
                return false;
            
            string cmPat = @"(\d+)cm";
            string inPat = @"(\d+)in";
            
            if (Regex.IsMatch(userHeight, cmPat))
            {
                return int.TryParse(Regex.Match(userHeight, cmPat).Groups[1].Value, out int cm) &&
                       cm >= 150 && cm <= 193;
            }

            if (Regex.IsMatch(userHeight, inPat))
            {
                return int.TryParse(Regex.Match(userHeight, inPat).Groups[1].Value, out int inches) &&
                       inches >= 59 && inches <= 76;                
            }

            return false;
        }

        private static bool ValidateDate(string date, int min, int max)
        {
            return date != null &&
                   int.TryParse(date, out int byr) &&
                   byr >= min &&
                   byr <= max;
        }
    }

    public class User
    {
        public string BirthYear { get; set; }
        public string IssueYear { get; set; }
        public string ExpirationYear { get; set; }
        public string Height { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string PassportId { get; set; }
        public string CountryId { get; set; }

        public void Clear()
        {
            BirthYear = null;
            IssueYear = null;
            ExpirationYear = null;
            Height = null;
            HairColor = null;
            EyeColor = null;
            PassportId = null;
            CountryId = null;
        }
    }
}