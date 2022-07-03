using Sudoku.Services.Strategies;

namespace Sudoku.Tests;

public class LockedDoubleLineTests
{
    Grid _testGrid;
    LockedDoubleLinesStrategy _strategy;

    public LockedDoubleLineTests()
    {
        _testGrid = SudokuGenerator.EmptySudoku();
        _strategy = new LockedDoubleLinesStrategy(new StandardSudokuRules());
    }

    [Fact]
    public void LockedDoubleLines_ReturnsEliminatedCellsInRow()
    {
        // Notes:
        // 11 |1  |11 
        // 11 |1  |11 
        //    |1  |   
        // ----------- 

        _testGrid.SetCellNote(new(0, 0), 1);
        _testGrid.SetCellNote(new(0, 1), 1);
        _testGrid.SetCellNote(new(1, 0), 1);
        _testGrid.SetCellNote(new(1, 1), 1);

        _testGrid.SetCellNote(new(0, 3), 1);
        _testGrid.SetCellNote(new(1, 3), 1);
        _testGrid.SetCellNote(new(2, 3), 1);

        _testGrid.SetCellNote(new(0, 6), 1);
        _testGrid.SetCellNote(new(0, 7), 1);
        _testGrid.SetCellNote(new(1, 6), 1);
        _testGrid.SetCellNote(new(1, 7), 1);

        IEnumerable<SudokuSolutionBase> solutions = _strategy.Solve(_testGrid);

        List<Elimination> expectedSolutions = new List<Elimination>()
            {
                new(0, 3, 1),
                new(1, 3, 1),
            };
        solutions.Should().BeEquivalentTo(expectedSolutions);
    }
}
