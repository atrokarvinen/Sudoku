namespace Sudoku.Tests;

public class SudokuSolverTests
{
    private const string SimpleSudokuFile = "Sudoku_simple.txt";
    private readonly ISudokuRules rules;
    private readonly ISudokuSolver solver;

    public SudokuSolverTests()
    {
        rules = new StandardSudokuRules();
        solver = new SudokuSolver(rules);
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

        //Cell solvedCell = solver.SolveNextStep(grid);
        solver.Solve(sudoku);
    }
}

