using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode
{
    public class Day_03 : BaseDay
    {
        private readonly List<string> _rows;
        
        public Day_03()
        {
            _rows = File.ReadAllLines(InputFilePath).ToList();
        }
        public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateSumPart1()}, part 1");

        public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateSumPart2()}, part 2");

        private string CalculateSumPart1()
        {
            var sum = 0;
            for (int i = 0; i < _rows.Count; i++)
            {
                var regex = new Regex(@"\d+");
                foreach (Match match in regex.Matches(_rows[i]))
                {
                    var value = int.Parse(match.Value);
                    var startIndex = match.Index;
                    var endIndex = startIndex + match.Value.Length - 1;

                    var searchCells = GetSearchGrid(i, startIndex, endIndex);
                    var bordersSpecialCharacter = FindSpecialCharacter(searchCells);
                    if (bordersSpecialCharacter)
                    {
                        sum += value;
                    }
                }
            }
            return sum.ToString();
        }

        private List<(int row, int col)> GetSearchGrid(int rowIndex, int startIndex, int endIndex)
        {
            var searchCells = new List<(int row, int col)>();
            for (int i = rowIndex - 1; i < rowIndex + 2; i++)
            {
                for (int j = startIndex - 1; j <= endIndex + 1; j++)
                {
                    searchCells.Add((i, j));
                }
            }
            return searchCells.Where(cell => cell.row >= 0 && cell.row < _rows.Count && cell.col >= 0 && cell.col < _rows[rowIndex].Length).ToList();
        }

        private bool FindSpecialCharacter(List<(int row, int col)> searchGrid)
        {
            foreach (var searchCell in searchGrid)
            {
                var cell = _rows[searchCell.row][searchCell.col];
                if (!char.IsNumber(cell) && cell != '.')
                {
                    return true;
                }
            }
            return false;
        }
        //private string CalculateSumPart1()
        //{
        //    var sum = 0;
        //    for (var index = 0; index < _input.Length; index++)
        //    {
        //        var currentLine = _input[index];
        //        var numbers = Regex.Split(currentLine, @"\D+");

        //        string previousLine = null;
        //        string nextLine = null;
        //        if (index > 0) previousLine = _input[index - 1];
        //        if (index < _input.Length - 1) nextLine = _input[index + 1];

        //        foreach (var number in numbers)
        //        {
        //            if (index == 17 && number == "926")
        //            {
        //                var test = 0;
        //            }
        //            if (string.IsNullOrEmpty(number)) continue;
        //            var foundIndex = currentLine.IndexOf(number, StringComparison.Ordinal);
        //            var prefixChar = foundIndex > 0 ? currentLine[foundIndex - 1] : ' ';
        //            if (_symbolRegex.IsMatch(prefixChar.ToString()))
        //            {
        //                sum += int.Parse(number);
        //                continue;
        //            }
        //            var suffixChar = foundIndex + number.Length < currentLine.Length ? currentLine[foundIndex + number.Length] : ' ';
        //            if (_symbolRegex.IsMatch(suffixChar.ToString()))
        //            {
        //                sum += int.Parse(number);
        //                continue;
        //            }

        //            var added = false;
        //            // Check previous line
        //            if (previousLine != null)
        //            {
        //                for (int previousLineIndex = foundIndex - 1; previousLineIndex <= foundIndex + number.Length && previousLineIndex < currentLine.Length; previousLineIndex++)
        //                {
        //                    if (previousLineIndex < 0) continue;
        //                    var checkChar = previousLine[previousLineIndex];
        //                    if (_symbolRegex.IsMatch(checkChar.ToString()))
        //                    {
        //                        sum += int.Parse(number);
        //                        added = true;
        //                        break;
        //                    }
        //                }
        //            }

        //            // Check next line
        //            if (nextLine == null || added) continue;
        //            for (int nextLineIndex = foundIndex - 1; nextLineIndex <= foundIndex + number.Length && nextLineIndex < currentLine.Length; nextLineIndex++)
        //            {
        //                if (nextLineIndex < 0) continue;
        //                var checkChar = nextLine[nextLineIndex];
        //                if (_symbolRegex.IsMatch(checkChar.ToString()))
        //                {
        //                    sum += int.Parse(number);
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    return sum.ToString();
        //}

        private string CalculateSumPart2()
        {
            var sum = 0;
            

            return sum.ToString();
        }
    }
}
