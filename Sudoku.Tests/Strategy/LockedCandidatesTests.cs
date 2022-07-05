using Sudoku.Services.Strategies;

namespace Sudoku.Tests.Strategy;

public class LockedCandidatesTests
{
    Grid _testGrid;
    LockedCandidatesStrategy _strategy;

    public LockedCandidatesTests()
    {
        _testGrid = SudokuGenerator.EmptySudoku();
        _strategy = new LockedCandidatesStrategy(new StandardSudokuRules());
    }

    [Fact]
    public void LockedTwoLines_Claiming_ReturnsEliminatedCellsInRow()
    {
        // Notes:
        // 1 |  |  |1 |  |  |  |  |1 |
        // 1 |  |  |1 |  |  |  |  |1 |
        //   |  |  |  |  |  |1 |1 |1 |
        // ---------------------------

        _testGrid.SetCellNote(new(0, 0), 1);
        _testGrid.SetCellNote(new(1, 0), 1);

        _testGrid.SetCellNote(new(0, 3), 1);
        _testGrid.SetCellNote(new(1, 3), 1);

        _testGrid.SetCellNote(new(0, 8), 1);
        _testGrid.SetCellNote(new(1, 8), 1);
        _testGrid.SetCellNote(new(2, 6), 1);
        _testGrid.SetCellNote(new(2, 7), 1);
        _testGrid.SetCellNote(new(2, 8), 1);

        IEnumerable<Elimination> solutions = _strategy.EliminateRows(_testGrid).Distinct();

        List<Elimination> expectedSolutions = new List<Elimination>()
        {
            new(0, 8, 1),
            new(1, 8, 1),
        };
        solutions.Should().BeEquivalentTo(expectedSolutions);
    }

    [Fact]
    public void LockedCandidates_Claiming_ReturnsEliminatedCellsInBox()
    {
        // Notes:
        // 1 |1 |  |  |  |  |  |  |  |
        //   |  | 1|  |  |  |  |  |  |
        //   |  |  |  |  |  |  |  |  |
        // ---------------------------

        _testGrid.SetCellNote(new(0, 0), 1);
        _testGrid.SetCellNote(new(0, 1), 1);

        _testGrid.SetCellNote(new(1, 2), 1);

        IEnumerable<Elimination> solutions = _strategy.Solve(_testGrid);

        solutions.Should().HaveCount(1);
        solutions.First().Should().BeEquivalentTo(new Elimination(1, 2, 1));
    }

    [Fact]
    public void LockedCandidates_Pointing_ReturnsEliminatedCellsInRow()
    {
        // Notes:
        // 1 |1 |  |  |  |  |  |  | 1|
        //   |  |  |  |  |  |  |  | 1|
        //   |  |  |  |  |  |  |  |  |
        // ---------------------------

        _testGrid.SetCellNote(new(0, 0), 1);
        _testGrid.SetCellNote(new(0, 1), 1);

        _testGrid.SetCellNote(new(0, 8), 1);
        _testGrid.SetCellNote(new(1, 8), 1);

        IEnumerable<Elimination> solutions = _strategy.Solve(_testGrid);

        solutions.Should().HaveCount(1);
        solutions.First().Should().BeEquivalentTo(new Elimination(0, 8, 1));
    }

    [Fact]
    public void LockedCandidates_Pointing_ReturnsEliminatedCellsInColumn()
    {
        _testGrid.SetCellNote(new(0, 0), 1);
        _testGrid.SetCellNote(new(1, 0), 1);

        _testGrid.SetCellNote(new(8, 0), 1);
        _testGrid.SetCellNote(new(8, 1), 1);

        IEnumerable<Elimination> solutions = _strategy.Solve(_testGrid);

        solutions.Should().HaveCount(1);
        solutions.First().Should().BeEquivalentTo(new Elimination(8, 0, 1));
    }
}
