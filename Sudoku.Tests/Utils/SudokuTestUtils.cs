using Sudoku.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Tests
{
    internal static class SudokuTestUtils
    {
        public const string TestSudokuFolder = "Saved test sudokus";

        public static Domain.Sudoku LoadSudokuFromFile(string fileName)
        {
            ISudokuProvider sudokuProvider = new SudokuFileProvider();
            string path = Path.Combine(TestSudokuFolder, fileName);
            return sudokuProvider.LoadSudoku(path);
        }
    }
}
