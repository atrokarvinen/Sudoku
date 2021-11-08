using Sudoku.Services;
using System;
using System.IO;
using Xunit;

namespace Sudoku.Tests
{
    public class SudokuRulesTest
    {
        ISudokuProvider _SudokuProvider;
        const string TestSudokuFolder = "Saved test sudokus";
        public SudokuRulesTest()
        {
            _SudokuProvider = new SudokuFileProvider();
        }

        [Fact]
        public void Test1()
        {
            Domain.Sudoku sudoku = _SudokuProvider.LoadSudoku(Path.Combine(TestSudokuFolder, "Sudoku_simple.txt"));
            //gameName = "Sudoku_2021_10_24__12_54_22.txt"
        }
    }
}
