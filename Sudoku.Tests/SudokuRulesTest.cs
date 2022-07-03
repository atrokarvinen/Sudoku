namespace Sudoku.Tests;

public class SudokuRulesTest
{
    private readonly ISudokuRules _sudokuRules;
    private readonly Grid _grid;

    private const string SUDOKU_TEXT =
        @" 1 |   |   
                 |   |   
                3|   |   
              -----------
                 |   |   
                 |   |   
                 |   |   
              -----------
                 |   |   
                 |   |  4
              2  |   |   ";

    public SudokuRulesTest()
    {
        _sudokuRules = new StandardSudokuRules();
        _grid = SudokuGenerator.FromText(SUDOKU_TEXT);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 0)]
    [InlineData(8, 8)]
    public void CellIsEmpty(int row, int column)
    {
        GridPoint gridPoint = new GridPoint(row, column);

        _sudokuRules.IsCellEmpty(_grid, gridPoint).Should().BeTrue();
    }

    [Fact]
    public void GetCellsInBox_TopLeft()
    {
        List<Cell> boxCells = _sudokuRules.GetCellsInBox(_grid, new GridPoint(0, 0));

        Assert.True(boxCells.Count == 9);
        Assert.True(boxCells.Min(x => x.Row) == 0);
        Assert.True(boxCells.Max(x => x.Row) == 2);
        Assert.True(boxCells.Min(x => x.Column) == 0);
        Assert.True(boxCells.Max(x => x.Column) == 2);
    }

    [Fact]
    public void GetCellsInBox_Center()
    {
        List<Cell> boxCells = _sudokuRules.GetCellsInBox(_grid, new GridPoint(4, 4));

        Assert.True(boxCells.Count == 9);
        Assert.True(boxCells.Min(x => x.Row) == 3);
        Assert.True(boxCells.Max(x => x.Row) == 5);
        Assert.True(boxCells.Min(x => x.Column) == 3);
        Assert.True(boxCells.Max(x => x.Column) == 5);
    }

    [Fact]
    public void GetCellsInBox_BottomRight()
    {
        List<Cell> boxCells = _sudokuRules.GetCellsInBox(_grid, new GridPoint(7, 7));

        Assert.True(boxCells.Count == 9);
        Assert.True(boxCells.Min(x => x.Row) == 6);
        Assert.True(boxCells.Max(x => x.Row) == 8);
        Assert.True(boxCells.Min(x => x.Column) == 6);
        Assert.True(boxCells.Max(x => x.Column) == 8);
    }

    [Theory]
    [InlineData(0, 0, 4)]
    [InlineData(6, 0, 5)]
    [InlineData(8, 8, 7)]
    public void BoxShouldAllowPlacement(int row, int column, int number)
    {
        GridPoint gridPoint = new GridPoint(row, column);
        Assert.True(_sudokuRules.DoesBoxAllowPlacement(_grid, gridPoint, number));
    }

    [Theory]
    [InlineData(0, 0, 1)]
    [InlineData(6, 0, 2)]
    [InlineData(8, 8, 4)]
    public void BoxShouldNotAllowPlacement(int row, int column, int number)
    {
        GridPoint gridPoint = new GridPoint(row, column);
        Assert.False(_sudokuRules.DoesBoxAllowPlacement(_grid, gridPoint, number));
    }

    [Theory]
    [InlineData(0, 0, 4)]
    [InlineData(1, 0, 4)]
    [InlineData(8, 8, 1)]
    public void CellShouldAllowPlacement(int row, int column, int number)
    {
        GridPoint gridPoint = new GridPoint(row, column);
        _sudokuRules.CanNumberBePlaced(_grid, gridPoint, number).Should().BeTrue();

    }

    [Theory]
    [InlineData(0, 0, 1)]
    [InlineData(2, 1, 3)]
    [InlineData(8, 8, 4)]
    public void CellShouldNotAllowPlacement(int row, int column, int number)
    {
        GridPoint gridPoint = new GridPoint(row, column);
        _sudokuRules.CanNumberBePlaced(_grid, gridPoint, number).Should().BeFalse();
    }

    [Fact(Skip = "In progress")]
    public void GetRelatedCells()
    {
        List<Cell> relatedCells = _sudokuRules.GetRelatedCells(_grid, new GridPoint(0, 0));

        List<GridPoint> expectedGridPoints = new List<GridPoint>()
            {
                // Box
                new GridPoint(0,0), new GridPoint(0,1), new GridPoint(0,2),
                new GridPoint(1,0), new GridPoint(1,1), new GridPoint(1,2),
                new GridPoint(2,0), new GridPoint(2,1), new GridPoint(2,2),

                // Column
                new GridPoint(0,3),
                new GridPoint(0,4),
                new GridPoint(0,5),
                new GridPoint(0,6),
                new GridPoint(0,7),
                new GridPoint(0,8),

                // Row
                new GridPoint(3,0),
                new GridPoint(4,0),
                new GridPoint(5,0),
                new GridPoint(6,0),
                new GridPoint(7,0),
                new GridPoint(8,0),

            };

        var relatedGridpoints = relatedCells.Select(x => x.GridPoint);
        Assert.Equal((3 * 3) + 6 + 6, relatedCells.Count);
        Assert.All(expectedGridPoints, (gridpoint) => Assert.Single(relatedGridpoints, gridpoint));
    }
}
