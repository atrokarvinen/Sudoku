using Sudoku.Domain;
using Sudoku.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Sudoku.Tests;

public class SudokuGridTests
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
    public void ConstructGridFromText()
    {
        Grid grid = new Grid(sudokuText);

        Assert.True(grid.Cells.SelectMany(x => x).Count() == 9 * 9);

        Assert.Null(grid.Cells[0][0].Number);
        Assert.Null(grid.Cells[0][3].Number);
        Assert.Null(grid.Cells[8][0].Number);

        Assert.True(grid.Cells[0][4].Number == 1);
        Assert.True(grid.Cells[0][5].Number == 8);
        Assert.True(grid.Cells[1][1].Number == 3);
        Assert.True(grid.Cells[1][2].Number == 8);
        Assert.True(grid.Cells[4][4].Number == 6);
        Assert.True(grid.Cells[4][5].Number == 9);
    }

    [Fact]
    public void ShowGridAsText()
    {
        Grid grid = new Grid(sudokuText);
        string sudokuAsText = grid.ShowAsText();

        Assert.Equal(sudokuText, sudokuAsText);
    }

    [Fact]
    public void AddNote()
    {
        Grid grid = new Grid(sudokuText);

        GridPoint gridPoint = new GridPoint(0, 0);
        grid.SetCellNote(gridPoint, 1);

        IEnumerable<int> notes = grid.GetCell(gridPoint).Notes;
        Assert.True(notes.Count() == 1);
        Assert.True(notes.First() == 1);
    }

    [Fact]
    public void ResetNotes()
    {
        Grid grid = new Grid(sudokuText);

        GridPoint gridPoint = new GridPoint(0, 0);
        grid.SetCellNote(gridPoint, 1);
        grid.SetCellNote(gridPoint, 2);
        grid.SetCellNote(gridPoint, 3);

        grid.ResetCellNotes(gridPoint);

        IEnumerable<int> notes = grid.GetCell(gridPoint).Notes;
        Assert.True(notes.Count() == 0);
    }
}
