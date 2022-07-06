using Sudoku.Services.Strategies;

namespace Sudoku.Tests.Strategy;

public class HiddenPairsTests
{
    Grid _testGrid = SudokuGenerator.EmptySudoku();
    HiddenSubsetStrategy _strategy = new HiddenSubsetStrategy(new StandardSudokuRules());

    [Fact]
    public void HiddenPairs_FindsPairInBox()
    {
        // Notes:
        // 368 |1368|    |3 |  |  |  |  |  |
        // 2379|1237|    |  |  |  |  |  |  |
        // 239 |123 |29  |9 |  |  |  |  |  |
        // -----------------------------

        _testGrid.SetCellNote(new(0, 0), 3);
        _testGrid.SetCellNote(new(0, 0), 6);
        _testGrid.SetCellNote(new(0, 0), 8);

        _testGrid.SetCellNote(new(0, 1), 1);
        _testGrid.SetCellNote(new(0, 1), 3);
        _testGrid.SetCellNote(new(0, 1), 6);
        _testGrid.SetCellNote(new(0, 1), 8);

        //_testGrid.SetCellNote(new(1, 0), 2);
        //_testGrid.SetCellNote(new(1, 0), 3);
        //_testGrid.SetCellNote(new(1, 0), 7);
        //_testGrid.SetCellNote(new(1, 0), 9);

        //_testGrid.SetCellNote(new(1, 1), 1);
        //_testGrid.SetCellNote(new(1, 1), 2);
        //_testGrid.SetCellNote(new(1, 1), 3);
        //_testGrid.SetCellNote(new(1, 1), 7);

        //_testGrid.SetCellNote(new(2, 0), 2);
        //_testGrid.SetCellNote(new(2, 0), 3);
        //_testGrid.SetCellNote(new(2, 0), 9);

        //_testGrid.SetCellNote(new(2, 1), 1);
        //_testGrid.SetCellNote(new(2, 1), 2);
        //_testGrid.SetCellNote(new(2, 1), 3);

        //_testGrid.SetCellNote(new(2, 2), 2);
        //_testGrid.SetCellNote(new(2, 2), 9);

        _testGrid.SetCellNote(new(0, 4), 3);
        //_testGrid.SetCellNote(new(0, 4), 9);

        IEnumerable<Elimination> solutions = _strategy.Solve(_testGrid);

        List<Elimination> expectedSolutions = new List<Elimination>()
        {
            new(0, 0, 3),
            new(0, 1, 1),
            new(0, 1, 3),
        };
        solutions.Should().BeEquivalentTo(expectedSolutions);
    }

    [Fact]
    public void HiddenPairs_DoesNotFindHiddenPair()
    {
        // Notes:
        // 123|1 |  |  |  |  |  |  |124|
        //    |  |  |  |  |  |  |  |   |
        //    |  |  |  |  |  |  |  |   |
        // -----------------------------

        _testGrid.SetCellNote(new(0, 0), 1);
        _testGrid.SetCellNote(new(0, 0), 2);
        _testGrid.SetCellNote(new(0, 0), 3);
        _testGrid.SetCellNote(new(0, 1), 1);

        _testGrid.SetCellNote(new(0, 8), 1);
        _testGrid.SetCellNote(new(0, 8), 2);
        _testGrid.SetCellNote(new(0, 8), 4);

        IEnumerable<Elimination> solutions = _strategy.Solve(_testGrid);

        solutions.Should().BeEmpty();
    }

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
