using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Services.Strategies;

public class BoxScanStrategy : ISudokuStrategy
{
    private readonly ISudokuRules _sudokuRules;

    public BoxScanStrategy(ISudokuRules sudokuRules)
    {
        _sudokuRules = sudokuRules;
    }

    public IEnumerable<SudokuSolutionBase> Solve(Grid sudoku)
    {
        List<Cell> cells = sudoku.GetCellsAsList();
        IEnumerable<Elimination> eliminations = cells
            .Where(cell => cell.Number is not null)
            .SelectMany(cell =>
            {
                int number = cell.Number.Value;
                GridPoint gridPoint = cell.GridPoint;
                IEnumerable<Cell> cells = _sudokuRules
                    .GetCellsInBox(sudoku, gridPoint);
                IEnumerable<Cell> eliminatedCells = cells
                    .Where(c =>  c.Notes.Contains(number));
                return eliminatedCells.Select(c => new Elimination(c.Row, c.Column, number));
            });
        return eliminations;
    }
}
