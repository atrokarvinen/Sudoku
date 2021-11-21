using Sudoku.Services;
using System;
using System.IO;
using Xunit;

namespace Sudoku.Tests;

public class SudokuSaveLoad
{
    string sudokuText = "-,-,-,-,1,8,-,-,-," +
                        "5,3,8,2,9,4,-,-,-," +
                        "9,-,6,7,-,5,4,2,-," +
                        "8,-,4,5,7,3,-,-,-," +
                        "1,-,-,4,6,9,-,-,-," +
                        "3,-,9,-,2,1,7,4,-," +
                        "-,-,1,3,-,-,9,8,4," +
                        "4,-,-,1,-,6,2,7,3," +
                        "-,8,-,-,-,2,-,6,1";

    [Fact]
    public void LoadSudoku()
    {
        ISudokuProvider _SudokuProvider = new SudokuFileProvider();
        string path = Path.Combine(SudokuTestUtils.TestSudokuFolder, "Sudoku_simple.txt");
        Domain.Sudoku sudoku = _SudokuProvider.LoadSudoku(path);

        string loadedSudokuText = sudoku.Grid.ShowAsText();
        Assert.Equal(sudokuText, loadedSudokuText);
    }
}
