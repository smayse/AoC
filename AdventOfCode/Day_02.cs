using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode
{
    public class Day_02 : BaseDay
    {
        private readonly string[] _input;
        private readonly int maxRed = 12;
        private readonly int maxGreen = 13;
        private readonly int maxBlue = 14;
        public Day_02()
        {
            _input = File.ReadAllLines(InputFilePath);
        }
        public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateSumPart1()}, part 1");

        public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateSumPart2()}, part 2");

        private string CalculateSumPart1()
        {
            var sum = 0;
            for (var index = 0; index < _input.Length; index++)
            {
                var line = _input[index];
                var game = line.Split(':');
                var rounds = game[1].Split(';');
                var possible = true;
                foreach (var round in rounds)
                {
                    var countRed = 0;
                    var countGreen = 0;
                    var countBlue = 0;
                    var colors = round.Split(',');
                    foreach (var color in colors)
                    {
                        var countString = Regex.Match(color, @"\d+").Value;
                        if (color.Contains("red"))
                        {
                            countRed += int.Parse(countString);
                            if (countRed <= maxRed) continue;
                            possible = false;
                            break;
                        }
                        else if (color.Contains("green"))
                        {
                            countGreen += int.Parse(countString);
                            if (countGreen <= maxGreen) continue;
                            possible = false;
                            break;
                        }
                        else if (color.Contains("blue"))
                        {
                            countBlue += int.Parse(countString);
                            if (countBlue <= maxBlue) continue;
                            possible = false;
                            break;
                        }
                    }
                    if (!possible) break;
                }
                if (possible) sum += index + 1;
            }

            return sum.ToString();
        }

        private string CalculateSumPart2()
        {
            var sum = 0;
            foreach (var line in _input)
            {
                var game = line.Split(':');
                var rounds = game[1].Split(';');
                var needRed = 0;
                var needGreen = 0;
                var needBlue = 0;
                foreach (var round in rounds)
                {
                    var colors = round.Split(',');
                    foreach (var color in colors)
                    {
                        var countString = Regex.Match(color, @"\d+").Value;
                        if (color.Contains("red"))
                        {
                            var countRed = int.Parse(countString);
                            if (countRed <= needRed) continue;
                            needRed = countRed;
                        }
                        else if (color.Contains("green"))
                        {
                            var countGreen = int.Parse(countString);
                            if (countGreen <= needGreen) continue;
                            needGreen = countGreen;
                        }
                        else if (color.Contains("blue"))
                        {
                            var countBlue = int.Parse(countString);
                            if (countBlue <= needBlue) continue;
                            needBlue = countBlue;
                        }
                    }
                }
                var power = needRed * needGreen * needBlue;
                sum += power;
            }

            return sum.ToString();
        }
    }
}
