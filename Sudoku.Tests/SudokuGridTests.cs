namespace Sudoku.Tests;

public class SudokuGridTests
{
    string sudokuText =
       @"   |  1|8  
         538|294|   
         9 6|7 5|42 
        ------------
         8 4|573|   
         1  |469|   
         3 9| 21|74 
        ------------
           1|3  |984
         4  |1 6|273
          8 |  2| 61";

    private readonly Grid _grid;

    public SudokuGridTests()
    {
        _grid = SudokuFromText.Convert(sudokuText);
    }

    [Fact]
    public void AddNote()
    {
        GridPoint gridPoint = new GridPoint(0, 0);
        _grid.SetCellNote(gridPoint, 1);

        IEnumerable<int> notes = _grid.GetCell(gridPoint).Notes;
        Assert.True(notes.Count() == 1);
        Assert.True(notes.First() == 1);
    }

    [Fact]
    public void ResetNotes()
    {
        GridPoint gridPoint = new GridPoint(0, 0);
        _grid.SetCellNote(gridPoint, 1);
        _grid.SetCellNote(gridPoint, 2);
        _grid.SetCellNote(gridPoint, 3);

        _grid.ResetCellNotes(gridPoint);

        IEnumerable<int> notes = _grid.GetCell(gridPoint).Notes;
        Assert.True(notes.Count() == 0);
    }
}
