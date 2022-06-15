﻿using Sudoku.Domain;
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
                IEnumerable<Cell> eliminatedCells = cells.Where(c => c.Row == row && c.Notes.Contains(number));
                return eliminatedCells.Select(c => new Elimination(row, c.Column, number));
            });
        return eliminations;
    }
}
