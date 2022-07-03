using Sudoku.Tests.Saved_test_sudokus;

namespace Sudoku.Tests;

public class SudokuSolverTests
{
    private const string SimpleSudokuFile = "Sudoku_simple.txt";
    private readonly ISudokuRules rules;
    private readonly ISudokuSolver solver;

    public SudokuSolverTests()
    {
        rules = new StandardSudokuRules();
        //solver = new SudokuSolver(rules);
        solver = new StrategySolver(rules);
    }

    [Fact]
    public void IsSolved_SolvedSudoku_ReturnsTrue()
    {
        Domain.Sudoku sudoku = SudokuTestUtils.LoadSudokuFromFile("Sudoku_solved.txt");
        Assert.True(solver.IsSudokuSolved(sudoku.Grid));
    }

    [Fact]
    public void IsSolved_IncompleteSudoku_ReturnsFalse()
    {
        Domain.Sudoku sudoku = SudokuTestUtils.LoadSudokuFromFile(SimpleSudokuFile);
        Assert.False(solver.IsSudokuSolved(sudoku.Grid));
    }

    [Fact]
    public void IsSolved_WronglySolvedSudoku_ReturnsFalse()
    {
        Domain.Sudoku sudoku = SudokuTestUtils.LoadSudokuFromFile("Sudoku_wrongly_solved.txt");
        Assert.False(solver.IsSudokuSolved(sudoku.Grid));
    }

    [Fact]
    public void TestSolve()
    {
        Domain.Sudoku sudoku = SudokuTestUtils.LoadSudokuFromFile(SimpleSudokuFile);
        Grid grid = sudoku.Grid;

        solver.Solve(sudoku);

        solver.IsSudokuSolved(grid).Should().BeTrue();
    }

    [Fact]
    public void TestSolveMedium()
    {
        var sudokeText = TestSudokuFixtures.MediumSudoku;
        var sudoku = new Domain.Sudoku() { Grid = SudokuGenerator.FromText(sudokeText.Sudoku) };

        solver.Solve(sudoku);

        solver.IsSudokuSolved(sudoku.Grid).Should().BeTrue();
    }

    [Fact]
    public void TestSolveHard()
    {
        var hardSudoku = TestSudokuFixtures.HardSudoku;
        var sudoku = new Domain.Sudoku() { Grid = SudokuGenerator.FromText(hardSudoku.Sudoku) };

        solver.Solve(sudoku);

        solver.IsSudokuSolved(sudoku.Grid).Should().BeTrue();
    }

    [Fact]
    public void TestSolveExpert()
    {
        var hardSudoku = TestSudokuFixtures.ExpertSudoku;
        var sudoku = new Domain.Sudoku() { Grid = SudokuGenerator.FromText(hardSudoku.Sudoku) };

        solver.Solve(sudoku);

        solver.IsSudokuSolved(sudoku.Grid).Should().BeTrue();
    }

    [Fact]
    public void TestSolveEvil()
    {
        var hardSudoku = TestSudokuFixtures.EvilSudoku;
        var sudoku = new Domain.Sudoku() { Grid = SudokuGenerator.FromText(hardSudoku.Sudoku) };

        solver.Solve(sudoku);

        solver.IsSudokuSolved(sudoku.Grid).Should().BeTrue();
    }

    [Theory]
    [InlineData("medium")]
    [InlineData("hard")]
    [InlineData("expert")]
    [InlineData("evil")]
    public void IsSolved_LegallySolvedSudoku_ReturnsTrue(string sudokuType)
    {
        string sudokuSolution;
        switch (sudokuType)
        {
            case "medium":
                sudokuSolution = TestSudokuFixtures.MediumSudoku.SolvedSudoku;
                break;
            case "hard":
                sudokuSolution = TestSudokuFixtures.HardSudoku.SolvedSudoku;
                break;
            case "expert":
                sudokuSolution = TestSudokuFixtures.ExpertSudoku.SolvedSudoku;
                break;
            case "evil":
                sudokuSolution = TestSudokuFixtures.EvilSudoku.SolvedSudoku;
                break;
            default:
                return;
        }
        Grid sudoku = SudokuGenerator.FromText(sudokuSolution);
        solver.IsSudokuSolved(sudoku).Should().BeTrue();
    }
}

