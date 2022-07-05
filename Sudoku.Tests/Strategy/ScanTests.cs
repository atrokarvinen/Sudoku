using Sudoku.Services.Strategies;

namespace Sudoku.Tests.Strategy;

public class ScanTests
{
    private readonly string sudokuText =
      @" 12| 5 |  3
            3  |   |   
               |   |   
            -----------
               |   |   
               |   |   
               |   |   
            -----------
               |   |   
               |   |   
              9|   |   ";

    Grid _testGrid;
    ScanStrategy _strategy;

    public ScanTests()
    {
        _testGrid = SudokuGenerator.FromText(sudokuText);
        _strategy = new ScanStrategy(new StandardSudokuRules());
    }

    [Theory]
    [InlineData(2, 0, 1)]
    [InlineData(2, 2, 3)]
    [InlineData(2, 3, 5)]
    [InlineData(1, 7, 3)]
    [InlineData(7, 0, 9)]
    public void BoxScanStrategy_EliminatesSingleNote(int row, int column, int number)
    {
        GridPoint targetPoint = new(row, column);
        _testGrid.SetCellNote(targetPoint, number);

        IEnumerable<SudokuSolutionBase> solutions = _strategy.SolveBoxes(_testGrid);

        solutions.Should().HaveCount(1);
        solutions.First().Should().BeEquivalentTo(new Elimination(row, column, number));
    }

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(4, 2, 2)]
    [InlineData(5, 4, 5)]
    [InlineData(7, 8, 3)]
    [InlineData(1, 2, 9)]
    public void ColumnScanStrategy_EliminatesSingleNote(int row, int column, int number)
    {
        GridPoint targetPoint = new(row, column);
        _testGrid.SetCellNote(targetPoint, number);

        IEnumerable<SudokuSolutionBase> solutions = _strategy.SolveColumns(_testGrid);

        solutions.Should().HaveCount(1);
        solutions.First().Should().BeEquivalentTo(new Elimination(row, column, number));
    }

    [Theory]
    [InlineData(0, 0, 1)]
    [InlineData(0, 7, 2)]
    [InlineData(0, 5, 5)]
    [InlineData(8, 8, 9)]
    public void RowScanStrategy_EliminatesSingleNote(int row, int column, int number)
    {
        GridPoint targetPoint = new(row, column);
        _testGrid.SetCellNote(targetPoint, number);

        IEnumerable<SudokuSolutionBase> solutions = _strategy.SolveRows(_testGrid);

        solutions.Should().HaveCount(1);
        solutions.First().Should().BeEquivalentTo(new Elimination(row, column, number));
    }

    [Fact]
    public void RowScanStrategy_EliminatesMultipleNotes()
    {
        List<Elimination> expectedSolutions = new List<Elimination>()
            {
                new Elimination(0, 0, 1),
                new Elimination(0, 7, 2),
                new Elimination(0, 5, 5),
                new Elimination(8, 8, 9),
            };
        expectedSolutions.ForEach(e => _testGrid.SetCellNote(e.Row, e.Column, e.Number));

        IEnumerable<SudokuSolutionBase> solutions = _strategy.SolveRows(_testGrid);

        solutions.Should().HaveCount(expectedSolutions.Count);
        solutions.Should().BeEquivalentTo(expectedSolutions);
    }
}
