using Sudoku.Domain;
using System.Collections.Generic;

namespace Sudoku.Services.Strategies;

public abstract record SudokuSolutionBase(int Row, int Column, int Number);
public record Elimination(int Row, int Column, int Number) : SudokuSolutionBase(Row, Column, Number);
public record Addition(int Row, int Column, int Number) : SudokuSolutionBase(Row, Column, Number);

public interface ISudokuStrategy
{
    IEnumerable<SudokuSolutionBase> Solve(Grid sudoku);
    void ApplySolution(Grid sudoku, SudokuSolutionBase solution);
}

public abstract class EliminationStrategyBase : ISudokuStrategy
{
    public void ApplySolution(Grid sudoku, SudokuSolutionBase solution)
    {
        var (row, column, number) = solution;
        sudoku.RemoveCellNote(new(row, column), number);
    }

    public abstract IEnumerable<SudokuSolutionBase> Solve(Grid sudoku);
}

public abstract class AdditionStrategyBase : ISudokuStrategy
{
    public void ApplySolution(Grid sudoku, SudokuSolutionBase solution)
    {
        var (row, column, number) = solution;
        GridPoint point = new GridPoint(row, column);
        sudoku.SetCellNumber(point, number);
        sudoku.ResetCellNotes(point);
    }

    public abstract IEnumerable<SudokuSolutionBase> Solve(Grid sudoku);
}