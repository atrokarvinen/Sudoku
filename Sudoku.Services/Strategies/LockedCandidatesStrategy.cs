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
    private enum Method
    {
        Pointing,
        Claiming
    }

    private readonly ISudokuRules _sudokuRules;

    public LockedCandidatesStrategy(ISudokuRules sudokuRules)
    {
        _sudokuRules = sudokuRules;
    }

    public override IEnumerable<Elimination> Solve(Grid sudoku)
    {
        IEnumerable<Elimination> rowEliminations = EliminateRows(sudoku);
        IEnumerable<Elimination> columnEliminations = EliminateColumns(sudoku);

        List<Elimination> allEliminations = new List<Elimination>();
        allEliminations.AddRange(rowEliminations);
        allEliminations.AddRange(columnEliminations);

        IEnumerable<Elimination> uniqueEliminations = allEliminations.Distinct();

        return uniqueEliminations;
    }

    public IEnumerable<Elimination> EliminateRows(Grid sudoku)
    {
        Func<Cell, Cell, bool> comparator = (cellA, cellB) => cellA.Row == cellB.Row;
        IEnumerable<Elimination> pointedCells = Eliminate(sudoku, comparator, Method.Pointing);
        IEnumerable<Elimination> claimedCells = Eliminate(sudoku, comparator, Method.Claiming);
        return pointedCells.Union(claimedCells);
    }

    public IEnumerable<Elimination> EliminateColumns(Grid sudoku)
    {
        Func<Cell, Cell, bool> comparator = (cellA, cellB) => cellA.Column == cellB.Column;
        Func<Cell, Cell, bool> otherCellsQuery = (cellA, cellB) => cellA.Row == cellB.Row;
        IEnumerable<Elimination> pointedCells = Eliminate(sudoku, comparator, Method.Pointing);
        IEnumerable<Elimination> claimedCells = Eliminate(sudoku, comparator, Method.Claiming);
        return pointedCells.Union(claimedCells);
    }

    private IEnumerable<Elimination> Eliminate(
        Grid sudoku, 
        Func<Cell, Cell, bool> lineQuery,
        Method method)
    {
        List<Elimination> eliminations = new List<Elimination>();
        List<Cell> cells = sudoku.GetCellsAsList();
        IEnumerable<Cell> emptyCells = cells.Where(cell => cell.Number is null);
        foreach (var emptyCell in emptyCells)
        {
            int boxIndex = _sudokuRules.GetCellBoxIndex(emptyCell);
            IEnumerable<Cell> cellsInSameBox = _sudokuRules.GetCellsInBox(sudoku, emptyCell.GridPoint);
            IEnumerable<Cell> cellsInSameBoxAndLine = cellsInSameBox.Where(cell => lineQuery(cell, emptyCell));
            IEnumerable<Cell> cellsInSameBoxDifferentLine = cellsInSameBox.Where(cell => !lineQuery(cell, emptyCell));
            IEnumerable<Cell> cellsInSameLineDifferentBox = cells
                .Where(cell => lineQuery(cell, emptyCell) && _sudokuRules.GetCellBoxIndex(cell) != boxIndex);

            HashSet<int> notesToTest = cellsInSameBoxAndLine
                .SelectMany(cell => cell.Notes)
                .GroupBy(note => note)
                .Where(notes => notes.Count() >= 2)
                .Select(noteGroup => noteGroup.First())
                .ToHashSet();

            IEnumerable<Cell> cellsToTestAgainst = method == Method.Pointing
                ? cellsInSameBoxDifferentLine
                : cellsInSameLineDifferentBox;
            HashSet<int> notesToTestAgainst = cellsToTestAgainst.SelectMany(cell => cell.Notes).ToHashSet();
            //HashSet<int> notesToTestAgainst = cellsInSameLineDifferentBox.SelectMany(cell => cell.Notes).ToHashSet();
            
            // Remove notes from other cells in the box
            notesToTest.ExceptWith(notesToTestAgainst);

            foreach (var note in notesToTest)
            {
                IEnumerable<Cell> eliminatedGroup = method == Method.Pointing
                    ? cellsInSameLineDifferentBox
                    : cellsInSameBoxDifferentLine;
                IEnumerable<Cell> eliminatedCells = eliminatedGroup.Where(cell => cell.Notes.Contains(note));
                //IEnumerable<Cell> eliminatedCells = cellsInSameBoxDifferentLine.Where(cell => cell.Notes.Contains(note));
                eliminations.AddRange(eliminatedCells.Select(cell => new Elimination(cell.Row, cell.Column, note)));
            }
        }
        return eliminations;
    }
}
