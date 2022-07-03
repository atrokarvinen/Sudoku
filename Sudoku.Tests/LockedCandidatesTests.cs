using Sudoku.Services.Strategies;

namespace Sudoku.Tests;

public class LockedCandidatesTests
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

    [Fact]
    public void LockedCandidatesStrategy_ReturnsEliminatedCells()
    {
        Grid testGrid = SudokuGenerator.EmptySudoku();
        GridPoint targetPoint = new(0, 5);
        testGrid.SetCellNote(targetPoint, 1);
        testGrid.SetCellNote(new(1, 5), 1);
        testGrid.SetCellNote(new(0, 0), 1);
        testGrid.SetCellNote(new(0, 1), 1);
        LockedCandidatesStrategy strategy = new LockedCandidatesStrategy(new StandardSudokuRules());

        IEnumerable<SudokuSolutionBase> solutions = strategy.Solve(testGrid);

        solutions.Should().HaveCount(1);
        solutions.First().Should().BeEquivalentTo(new Elimination(0, 5, 1));
    }

    [Fact]
    public void LockedCandidatesStrategy_ReturnsEliminatedCellsInColumn()
    {
        Grid testGrid = SudokuGenerator.EmptySudoku();
        GridPoint targetPoint = new(0, 5);
        testGrid.SetCellNote(targetPoint, 1);
        testGrid.SetCellNote(new(1, 5), 1);
        testGrid.SetCellNote(new(8, 5), 1);
        testGrid.SetCellNote(new(8, 4), 1);
        LockedCandidatesStrategy strategy = new LockedCandidatesStrategy(new StandardSudokuRules());

        IEnumerable<SudokuSolutionBase> solutions = strategy.Solve(testGrid);

        solutions.Should().HaveCount(1);
        solutions.First().Should().BeEquivalentTo(new Elimination(8, 5, 1));
    }
}
