using Sudoku.Services.Strategies;

namespace Sudoku.Tests.Strategy;

public class HiddenPairsTests
{
    Grid _testGrid = SudokuGenerator.EmptySudoku();
    HiddenSubsetStrategy _strategy = new HiddenSubsetStrategy(new StandardSudokuRules());

    [Fact]
    public void HiddenPairs_ReturnsEliminatedCellsInRow()
    {
        // Notes:
        // 123|  |  |  |  |  |  |  |124|
        //    |  |  |  |  |  |  |  |   |
        //    |  |  |  |  |  |  |  |   |
        // -----------------------------

        _testGrid.SetCellNote(new(0, 0), 1);
        _testGrid.SetCellNote(new(0, 0), 2);
        _testGrid.SetCellNote(new(0, 0), 3);

        _testGrid.SetCellNote(new(0, 8), 1);
        _testGrid.SetCellNote(new(0, 8), 2);
        _testGrid.SetCellNote(new(0, 8), 4);

        IEnumerable<Elimination> solutions = _strategy.Solve(_testGrid);

        List<Elimination> expectedSolutions = new List<Elimination>()
        {
            new(0, 0, 3),
            new(0, 8, 4),
        };
        solutions.Should().BeEquivalentTo(expectedSolutions);
    }

}
