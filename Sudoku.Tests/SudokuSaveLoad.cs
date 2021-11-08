using Sudoku.Services;
using System;
using System.IO;
using Xunit;

namespace Sudoku.Tests
{
    public class SudokuSaveLoad
    {
        const string TestSudokuFolder = "Saved test sudokus";

        [Fact]
        public void LoadSudoku()
        {
            ISudokuProvider _SudokuProvider = new SudokuFileProvider();
            Domain.Sudoku sudoku = _SudokuProvider.LoadSudoku(Path.Combine(TestSudokuFolder, "Sudoku_simple.txt"));

        }
    }
}
