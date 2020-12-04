using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2020.Days
{
    class Day04
    {
        public Day04()
        {
            string input = File.ReadAllText("Days/Input/Day04.txt");
            string[] entrys = input.Split(new []{"\r\n\r\n"}, StringSplitOptions.RemoveEmptyEntries);

            int validPartA = 0;
            int validPartB = 0;

            /*
             * group : key
             * 1 : byr
             * 2 : iyr
             * 3 : eyr
             * 4 : hgt
             * 5 : hcl
             * 6 : ecl
             * 7 : pid
             * 8 : cid (optional)
             */
            Regex passportRegex = new Regex(@"^(?=.*byr:([^ ]+))(?=.*iyr:([^ ]+))(?=.*eyr:([^ ]+))(?=.*hgt:([^ ]+))(?=.*hcl:([^ ]+))(?=.*ecl:([^ ]+))(?=.*pid:([^ ]+))(?=.*cid:([^ ]+))?", RegexOptions.Compiled);
            Regex hgtRegex = new Regex(@"^(\d+)(cm|in)$", RegexOptions.Compiled);
            Regex hclRegex = new Regex(@"^#[0-9a-f]{6}$", RegexOptions.Compiled);
            Regex pidRegex = new Regex(@"^[0-9]{9}$", RegexOptions.Compiled);
            
            string[] ecl = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            foreach (string passport in entrys)
            {
                var match = passportRegex.Match(passport.Replace("\r\n", " "));
                if (match.Success)
                {
                    validPartA++;

                    bool valid = true;
                    valid &= int.TryParse(match.Groups[1].Value, out int byr) && byr >= 1920 && byr <= 2002;
                    valid &= int.TryParse(match.Groups[2].Value, out int iyr) && iyr >= 2010 && iyr <= 2020;
                    valid &= int.TryParse(match.Groups[3].Value, out int eyr) && eyr >= 2020 && eyr <= 2030;

                    var hgtMatch = hgtRegex.Match(match.Groups[4].Value);
                    valid &= hgtMatch.Success && int.TryParse(hgtMatch.Groups[1].Value, out int hgt) && (
                             (hgtMatch.Groups[2].Value == "cm" && hgt >= 150 && hgt <= 193) ||
                             (hgtMatch.Groups[2].Value == "in" && hgt >= 59 && hgt <= 76));

                    valid &= hclRegex.IsMatch(match.Groups[5].Value);

                    valid &= ecl.Contains(match.Groups[6].Value);
                    valid &= pidRegex.IsMatch(match.Groups[7].Value);


                    if (valid)
                    {
                        validPartB++;
                    }
                }
            }

            Console.WriteLine($"Part A: {validPartA}");
            Console.WriteLine($"Part B: {validPartB}");

        }
    }
}
