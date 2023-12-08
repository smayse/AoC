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
        

        private string CalculateSumPart2()
        {
            var gearLocations = new List<(int row, int col, int partNumber)>();

            for (int i = 0; i < _rows.Count; i++)
            {
                var regex = new Regex(@"\d+");
                foreach (Match match in regex.Matches(_rows[i]))
                {
                    var part = int.Parse(match.Value);
                    var startIndex = match.Index;
                    var endIndex = startIndex + match.Value.Length - 1;

                    var searchCells = GetSearchGrid(i, startIndex, endIndex);
                    var gears = FindSpecialCharacter(searchCells, part);
                    gearLocations.AddRange(gears);
                }
            }

            var sum = gearLocations
                .GroupBy(gearLocation => new
                {
                    gearLocation.row,
                    gearLocation.col,
                })
                .Select(gearLocationGroup => new
                {
                    row = gearLocationGroup.Key.row,
                    col = gearLocationGroup.Key.col,
                    partNumbers = gearLocationGroup.Select(glg => glg.partNumber).ToList(),
                })
                .Where(gearLocationGroup => gearLocationGroup.partNumbers.Count() == 2)
                .Select(gearLocationGroup => gearLocationGroup.partNumbers[0] * gearLocationGroup.partNumbers[1])
                .Sum();
            return sum.ToString();
        }

        private List<(int row, int col, int part)> FindSpecialCharacter(List<(int row, int col)> searchGrid, int part)
        {
            var symbols = new List<(int row, int col, int partNumber)>();
            foreach (var searchCell in searchGrid)
            {
                var cell = _rows[searchCell.row][searchCell.col];
                if (cell == '*')
                {
                    symbols.Add((searchCell.row, searchCell.col, part));
                }
            }
            return symbols;
        }
    }
}
