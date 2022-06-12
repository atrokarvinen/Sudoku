using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Services.Strategies;

public class RowScanStrategy : ISudokuStrategy
{
    public IEnumerable<SudokuSolutionBase> Solve(Grid sudoku)
    {
        List<Cell> cells = sudoku.GetCellsAsList();
        IEnumerable<Elimination> eliminations = cells
            .Where(cell => cell.Number is not null)
            .SelectMany(cell =>
            {
                int number = cell.Number.Value;
                int row = cell.Row;
                var notesWithSameNumberAndRow = cells.Where(cell => cell.Row == row && cell.Notes.Contains(number));

                return notesWithSameNumberAndRow.Select(cell => new Elimination(row, cell.Column, number));
            });
        return eliminations;
    }
}
