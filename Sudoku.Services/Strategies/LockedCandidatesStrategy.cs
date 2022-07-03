using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Services.Strategies;

/// <summary>
/// When a candidate is possible in a certain block and row/column, 
/// and it is not possible anywhere else in the same row/column, 
/// then it is also not possible anywhere else in the same block.
/// </summary>
public class LockedCandidatesStrategy : EliminationStrategyBase
{
    private readonly ISudokuRules _sudokuRules;

    public LockedCandidatesStrategy(ISudokuRules sudokuRules)
    {
        _sudokuRules = sudokuRules;
    }

    public override IEnumerable<SudokuSolutionBase> Solve(Grid sudoku)
    {
        IEnumerable<Elimination> rowEliminations = EliminateRows(sudoku);
        IEnumerable<Elimination> columnEliminations = EliminateColumns(sudoku);

        List<Elimination> allEliminations = new List<Elimination>();
        allEliminations.AddRange(rowEliminations);
        allEliminations.AddRange(columnEliminations);

        IEnumerable<Elimination> uniqueEliminations = allEliminations.Distinct();
            //.GroupBy(e => e.GridPoint)
            //.Select(group => group.First());

        return uniqueEliminations;
    }

    private IEnumerable<Elimination> EliminateRows(Grid sudoku)
    {
        Func<Cell, Cell, bool> comparator = (cellA, cellB) => cellA.Row == cellB.Row;
        return Eliminate(sudoku, comparator);
    }

    private IEnumerable<Elimination> EliminateColumns(Grid sudoku)
    {
        Func<Cell, Cell, bool> comparator = (cellA, cellB) => cellA.Column == cellB.Column;
        return Eliminate(sudoku, comparator);
    }

    private IEnumerable<Elimination> Eliminate(Grid sudoku, Func<Cell, Cell, bool> comparator)
    {
        List<Elimination> eliminations = new List<Elimination>();
        List<Cell> cells = sudoku.GetCellsAsList();
        IEnumerable<Cell> emptyCells = cells.Where(cell => cell.Number is null);
        foreach (var emptyCell in emptyCells)
        {
            IEnumerable<Cell> cellsInSameBox = _sudokuRules.GetCellsInBox(sudoku, emptyCell.GridPoint);
            IEnumerable<Cell> cellsInSameBoxAndLine = cellsInSameBox.Where(cell => comparator(cell, emptyCell));
            IEnumerable<Cell> cellsInSameBoxDifferentLine = cellsInSameBox.Where(cell => !comparator(cell, emptyCell));
            HashSet<int> notesToTest = cellsInSameBoxAndLine.SelectMany(cell => cell.Notes).ToHashSet();
            HashSet<int> notesToTestAgainst = cellsInSameBoxDifferentLine.SelectMany(cell => cell.Notes).ToHashSet();
            notesToTest.ExceptWith(notesToTestAgainst);

            int boxIndex = _sudokuRules.GetCellBoxIndex(emptyCell);
            IEnumerable<Cell> cellsInSameLineDifferentBox = cells.Where(cell => comparator(cell, emptyCell))
                .Where(cell => _sudokuRules.GetCellBoxIndex(cell) != boxIndex);
            foreach (var note in notesToTest)
            {
                IEnumerable<Cell> eliminatedCells = cellsInSameLineDifferentBox.Where(cell => cell.Notes.Contains(note));
                eliminations.AddRange(eliminatedCells.Select(cell => new Elimination(cell.Row, cell.Column, note)));
            }
        }
        return eliminations;
    }
}
