using Sudoku.Services.Strategies;

namespace Sudoku.Tests.Strategy;

public class SingleCandidateTests
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

    SingleCandidateStrategy _strategy;
    Grid _testGrid;

    public SingleCandidateTests()
    {
        _strategy = new SingleCandidateStrategy(new StandardSudokuRules());
        _testGrid = SudokuGenerator.EmptySudoku();
    }

    [Fact]
    public void SingleCandidateBoxStrategy_ReturnsOnlyCandidateInBox()
    {
        _testGrid.SetCellNote(new(0, 0), 1);
        _testGrid.SetCellNote(new(0, 0), 2);
        _testGrid.SetCellNote(new(1, 1), 2);

        IEnumerable<Addition> solutions = _strategy.SolveBoxes(_testGrid);

        solutions.Should().BeEquivalentTo(new List<Addition>()
            {
                new Addition(0, 0, 1)
            });
    }

    [Fact]
    public void SingleCandidateBoxStrategy_DoesNotFindSingleCandidate()
    {
        GridPoint targetPoint = new(1, 5);
        _testGrid.SetCellNote(targetPoint, 1);
        _testGrid.SetCellNote(new(2, 4), 1);

        IEnumerable<SudokuSolutionBase> solutions = _strategy.SolveBoxes(_testGrid);

        solutions.Should().BeEmpty();
    }

    [Fact]
    public void SingleCandidateColumnStrategy_ReturnsOnlyCandidateInColumn()
    {
        GridPoint targetPoint = new(1, 5);
        _testGrid.SetCellNote(targetPoint, 1);
        _testGrid.SetCellNote(targetPoint, 2);
        _testGrid.SetCellNote(targetPoint, 3);

        IEnumerable<SudokuSolutionBase> solutions = _strategy.SolveColumns(_testGrid);

        solutions.Should().BeEquivalentTo(new List<Addition>()
            {
                new Addition(1, 5, 1)
            });
    }

    [Fact]
    public void SingleCandidateColumnStrategy_DoesNotFindSingleCandidate()
    {
        GridPoint targetPoint = new(1, 5);
        _testGrid.SetCellNote(targetPoint, 1);
        _testGrid.SetCellNote(new(5, 5), 1);

        IEnumerable<SudokuSolutionBase> solutions = _strategy.SolveColumns(_testGrid);

        solutions.Should().BeEmpty();
    }

    [Fact]
    public void SingleCandidateRowStrategy_ReturnsOnlyCandidateInRow()
    {
        GridPoint targetPoint = new(1, 5);
        _testGrid.SetCellNote(targetPoint, 1);
        _testGrid.SetCellNote(targetPoint, 2);
        _testGrid.SetCellNote(targetPoint, 3);

        IEnumerable<SudokuSolutionBase> solutions = _strategy.SolveRows(_testGrid);

        solutions.Should().HaveCount(1);
        solutions.First().Should().BeEquivalentTo(new Addition(1, 5, 1));
    }

    [Fact]
    public void SingleCandidateRowStrategy_DoesNotFindSingleCandidate()
    {
        GridPoint targetPoint = new(1, 5);
        _testGrid.SetCellNote(targetPoint, 1);
        _testGrid.SetCellNote(new(1, 3), 1);

        IEnumerable<SudokuSolutionBase> solutions = _strategy.SolveRows(_testGrid);

        solutions.Should().BeEmpty();
    }

    [Fact]
    public void SingleCandidate_ReturnsOnlySolution()
    {
        GridPoint targetPoint = new(1, 5);
        _testGrid.SetCellNote(targetPoint, 1);
        SingleCandidateStrategy strategy = new SingleCandidateStrategy(new StandardSudokuRules());

        IEnumerable<SudokuSolutionBase> solutions = strategy.SolveSingleCells(_testGrid);

        solutions.Should().HaveCount(1);
        solutions.First().Should().BeEquivalentTo(new Addition(1, 5, 1));
    }
}
