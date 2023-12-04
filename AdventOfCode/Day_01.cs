using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode
{
    public class Day_01 : BaseDay
    {
        private readonly string[] _input;
        private readonly string[] _writtenDigits = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        public Day_01()
        {
            _input = File.ReadAllLines(InputFilePath);
        }
        public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateSum1()}, part 1");

        public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateSum2()}, part 2");

        private string CalculateSum1()
        {
            var sum = 0;
            foreach (var line in _input)
            {
                var result = line.Replace(_writtenDigits[0], "one1one").Replace(_writtenDigits[1], "two2two").Replace(_writtenDigits[2], "three3three").Replace(_writtenDigits[3], "four4four").Replace(_writtenDigits[4], "five5five").Replace(_writtenDigits[5], "six6six").Replace(_writtenDigits[6], "seven7seven").Replace(_writtenDigits[7], "eight8eight").Replace(_writtenDigits[8], "nine9nine");
                var numbers = new string(result.Where(char.IsDigit).ToArray());
                int.TryParse(numbers.First().ToString(), out var first);
                int.TryParse(numbers.Last().ToString(), out var last);
                int add = (first * 10) + last;
                sum += add;
            }

            return sum.ToString();
        }

        private string CalculateSum2()
        {
            var sum = 0;
            foreach (var line in _input)
            {
                var result = line.Replace(_writtenDigits[0], "one1one").Replace(_writtenDigits[1], "two2two").Replace(_writtenDigits[2], "three3three").Replace(_writtenDigits[3], "four4four").Replace(_writtenDigits[4], "five5five").Replace(_writtenDigits[5], "six6six").Replace(_writtenDigits[6], "seven7seven").Replace(_writtenDigits[7], "eight8eight").Replace(_writtenDigits[8], "nine9nine");
                int firstDigit = 0;
                int lastDigit = 0;
                int number = Convert.ToInt32(Regex.Replace(result, @"\D", ""));
                lastDigit = System.Math.Abs(number % 10);
                while (number != 0)
                {
                    firstDigit = System.Math.Abs(number % 10);
                    number /= 10;
                }
                int add = (firstDigit * 10) + lastDigit;
                sum += add;
            }

            return sum.ToString();
        }
    }
}
