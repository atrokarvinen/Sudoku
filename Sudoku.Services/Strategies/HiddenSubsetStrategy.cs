using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Services.Strategies;

/// <summary>
/// When n candidates are possible in a certain set of n cells all in the same
/// block, row, or column, and those n candidates are not possible elsewhere 
/// in that same block, row, or column, then no other candidates are possible in those cells.
/// </summary>
public class HiddenSubsetStrategy : EliminationStrategyBase
{
    public override IEnumerable<SudokuSolutionBase> Solve(Grid sudoku)
    {
        List<Cell> cells = sudoku.GetCellsAsList();
        IEnumerable<Elimination> eliminations = cells
            .Where(cell => cell.Number is not null)
            .SelectMany(cell =>
            {
                int number = cell.Number.Value;
                int row = cell.Row;
                IEnumerable<Cell> eliminatedCells = cells.Where(c => c.Row == row && c.Notes.Contains(number));
                return eliminatedCells.Select(c => new Elimination(row, c.Column, number));
            });
        return eliminations;
    }
}
