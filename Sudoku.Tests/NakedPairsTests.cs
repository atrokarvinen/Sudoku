using Sudoku.Services.Strategies;

namespace Sudoku.Tests;

public class NakedPairsTests
{
    Grid _testGrid = SudokuGenerator.EmptySudoku();

    [Fact]
    public void MultipleNakedPairs_EliminatesAll()
    {
        // Notes:
        // 12 |12 |   |
        // 34 |34 |134|
        //    |   |123|
        // ---------------------------

        _testGrid.SetCellNote(new(0, 0), 1);
        _testGrid.SetCellNote(new(0, 0), 2);
        _testGrid.SetCellNote(new(0, 1), 1);
        _testGrid.SetCellNote(new(0, 1), 2);

        _testGrid.SetCellNote(new(1, 0), 3);
        _testGrid.SetCellNote(new(1, 0), 4);
        _testGrid.SetCellNote(new(1, 1), 3);
        _testGrid.SetCellNote(new(1, 1), 4);

        _testGrid.SetCellNote(new(1, 2), 1);
        _testGrid.SetCellNote(new(1, 2), 3);
        _testGrid.SetCellNote(new(1, 2), 4);

        _testGrid.SetCellNote(new(2, 2), 1);
        _testGrid.SetCellNote(new(2, 2), 2);
        _testGrid.SetCellNote(new(2, 2), 3);

        NakedSubsetStrategy strategy = new NakedSubsetStrategy(new StandardSudokuRules());

        IEnumerable<SudokuSolutionBase> solutions = strategy.Solve(_testGrid);

        List<Elimination> expectedSolutions = new List<Elimination>()
            {
                new(1, 2, 1),
                new(1, 2, 3),
                new(1, 2, 4),
                new(2, 2, 1),
                new(2, 2, 2),
                new(2, 2, 3),
            };
        solutions.Should().BeEquivalentTo(expectedSolutions);
    }

    [Fact]
    public void NakedTriplet_EliminatesCellsInBox()
    {
        // Notes:
        // 123|123|123|
        //    |   |   |
        // 23 |   |   |
        // ---------------------------

        _testGrid.SetCellNote(new(0, 0), 1);
        _testGrid.SetCellNote(new(0, 0), 2);
        _testGrid.SetCellNote(new(0, 0), 3);
        _testGrid.SetCellNote(new(0, 1), 1);
        _testGrid.SetCellNote(new(0, 1), 2);
        _testGrid.SetCellNote(new(0, 1), 3);
        _testGrid.SetCellNote(new(0, 2), 1);
        _testGrid.SetCellNote(new(0, 2), 2);
        _testGrid.SetCellNote(new(0, 2), 3);

        _testGrid.SetCellNote(new(2, 0), 2);
        _testGrid.SetCellNote(new(2, 0), 3);

        NakedSubsetStrategy strategy = new NakedSubsetStrategy(new StandardSudokuRules());

        IEnumerable<SudokuSolutionBase> solutions = strategy.Solve(_testGrid);

        List<Elimination> expectedSolutions = new List<Elimination>()
            {
                new(2, 0, 2),
                new(2, 0, 3),
            };
        solutions.Should().BeEquivalentTo(expectedSolutions);
    }

    [Fact]
    public void RowAndColumnSameEliminations_DoesNotReturnDuplicates()
    {
        // Notes:
        // 123|  |  |  |  |  |  |12|12|
        //    |  |  |  |  |  |  |  |  |
        //    |  |  |  |  |  |  |  |  |
        // ---------------------------
        //    |  |  |
        //    |  |  |
        //    |  |  |
        // ---------------------------
        //    |  |  |
        // 12 |  |  |
        // 12 |  |  |
        // ---------------------------

        _testGrid.SetCellNote(new(0, 0), 1);
        _testGrid.SetCellNote(new(0, 0), 2);
        _testGrid.SetCellNote(new(0, 0), 3);

        _testGrid.SetCellNote(new(0, 7), 1);
        _testGrid.SetCellNote(new(0, 7), 2);
        _testGrid.SetCellNote(new(0, 8), 1);
        _testGrid.SetCellNote(new(0, 8), 2);

        _testGrid.SetCellNote(new(7, 0), 1);
        _testGrid.SetCellNote(new(7, 0), 2);
        _testGrid.SetCellNote(new(8, 0), 1);
        _testGrid.SetCellNote(new(8, 0), 2);

        NakedSubsetStrategy strategy = new NakedSubsetStrategy(new StandardSudokuRules());

        IEnumerable<SudokuSolutionBase> solutions = strategy.Solve(_testGrid);

        List<Elimination> expectedSolutions = new List<Elimination>()
            {
                new(0, 0, 1),
                new(0, 0, 2),
            };
        solutions.Should().BeEquivalentTo(expectedSolutions);
    }

    [Fact]
    public void NakedPairs_ReturnsEliminatedCellsInBox()
    {
        // Notes:
        // 12 |12 |   |
        //    |   |123|
        //    |   |   |
        // ---------------------------


        _testGrid.SetCellNote(new(0, 0), 1);
        _testGrid.SetCellNote(new(0, 0), 2);

        _testGrid.SetCellNote(new(0, 1), 1);
        _testGrid.SetCellNote(new(0, 1), 2);

        _testGrid.SetCellNote(new(1, 2), 1);
        _testGrid.SetCellNote(new(1, 2), 2);
        _testGrid.SetCellNote(new(1, 2), 3);

        NakedSubsetStrategy strategy = new NakedSubsetStrategy(new StandardSudokuRules());

        IEnumerable<SudokuSolutionBase> solutions = strategy.Solve(_testGrid);

        List<Elimination> expectedSolutions = new List<Elimination>()
            {
                new(1, 2, 1),
                new(1, 2, 2),
            };
        solutions.Should().BeEquivalentTo(expectedSolutions);
    }

    [Fact]
    public void NakedPairs_ReturnsEliminatedCellsInColumn()
    {

        _testGrid.SetCellNote(new(0, 0), 1);
        _testGrid.SetCellNote(new(0, 0), 2);

        _testGrid.SetCellNote(new(3, 0), 1);
        _testGrid.SetCellNote(new(4, 0), 2);

        _testGrid.SetCellNote(new(8, 0), 1);
        _testGrid.SetCellNote(new(8, 0), 2);

        NakedSubsetStrategy strategy = new NakedSubsetStrategy(new StandardSudokuRules());

        IEnumerable<SudokuSolutionBase> solutions = strategy.Solve(_testGrid);

        List<Elimination> expectedSolutions = new List<Elimination>()
            {
                new(3, 0, 1),
                new(4, 0, 2),
            };
        solutions.Should().BeEquivalentTo(expectedSolutions);
    }

    [Fact]
    public void NakedPairs_ReturnsEliminatedCellsInRow()
    {
        // Notes:
        // 12|  |  |1 | 2|  |  |  |12|
        //   |  |  |  |  |  |  |  |  |
        //   |  |  |  |  |  |  |  |  |
        // ---------------------------


        _testGrid.SetCellNote(new(0, 0), 1);
        _testGrid.SetCellNote(new(0, 0), 2);

        _testGrid.SetCellNote(new(0, 3), 1);
        _testGrid.SetCellNote(new(0, 4), 2);

        _testGrid.SetCellNote(new(0, 8), 1);
        _testGrid.SetCellNote(new(0, 8), 2);

        NakedSubsetStrategy strategy = new NakedSubsetStrategy(new StandardSudokuRules());

        IEnumerable<SudokuSolutionBase> solutions = strategy.Solve(_testGrid);

        List<Elimination> expectedSolutions = new List<Elimination>()
            {
                new(0, 3, 1),
                new(0, 4, 2),
            };
        solutions.Should().BeEquivalentTo(expectedSolutions);
    }

}
