using Sudoku.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Services.Strategies;

/// <summary>
/// When n candidates are possible in a certain set of n cells all in the same 
/// block, row, or column, and no other candidates are possible in those cells, 
/// then those n candidates are not possible elsewhere in that same block, row, or column.
/// </summary>
public class NakedSubsetStrategy : EliminationStrategyBase
{
    private readonly ISudokuRules _sudokuRules;

    public NakedSubsetStrategy(ISudokuRules sudokuRules)
    {
        _sudokuRules = sudokuRules;
    }

    public override IEnumerable<Elimination> Solve(Grid sudoku)
    {
        IEnumerable<Elimination> rowEliminations = EliminateRows(sudoku);
        IEnumerable<Elimination> columnEliminations = EliminateColumns(sudoku);
        IEnumerable<Elimination> boxEliminations = EliminateBoxes(sudoku);

        List<Elimination> allEliminations = new List<Elimination>();
        allEliminations.AddRange(rowEliminations);
        allEliminations.AddRange(columnEliminations);
        allEliminations.AddRange(boxEliminations);

        IEnumerable<Elimination> uniqueEliminations = allEliminations.Distinct();

        return uniqueEliminations;
    }

    private IEnumerable<Elimination> EliminateRows(Grid sudoku)
    {
        Func<Cell, int> groupBy = cell => cell.Row;
        return Eliminate(sudoku, groupBy);
    }

    private IEnumerable<Elimination> EliminateColumns(Grid sudoku)
    {
        Func<Cell, int> groupBy = cell => cell.Column;
        return Eliminate(sudoku, groupBy);
    }

    private IEnumerable<Elimination> EliminateBoxes(Grid sudoku)
    {
        Func<Cell, int> groupBy = cell => _sudokuRules.GetCellBoxIndex(cell);
        return Eliminate(sudoku, groupBy);
    }

    private IEnumerable<Elimination> Eliminate(Grid sudoku, Func<Cell, int> groupBy)
    {
        List<Elimination> eliminations = new List<Elimination>();
        List<Cell> cells = sudoku.GetCellsAsList();
        IEnumerable<Cell> emptyCells = cells.Where(cell => cell.Number is null);

        var cellGroups = emptyCells.GroupBy(groupBy);
        foreach (var cellGroup in cellGroups)
        {
            var partialEliminations = IterateSubsets(cellGroup);
            eliminations.AddRange(partialEliminations);
        }

        return eliminations;
    }

    private IEnumerable<Elimination> IterateSubsets(IEnumerable<Cell> cellGroup)
    {
        List<Elimination> eliminations = new List<Elimination>();

        int minSetSize = 2;
        int maxSetSize = 4;
        IEnumerable<int> setSizes = Enumerable.Range(2, maxSetSize - minSetSize + 1);
        foreach (int setSize in setSizes)
        {
            Dictionary<GridPoint, IEnumerable<int>> notesByPoint =
                cellGroup.ToDictionary(c => c.GridPoint, c => c.Notes);
            IEnumerable<GridPoint> points = cellGroup
                .Where(c => c.Notes.Count() == setSize)
                .Select(c => c.GridPoint);
            var sets = new Math.Combinatorics().GetAllSubsets(points, setSize);
            var setsWithSameNotes = sets.Where(set =>
            {
                HashSet<int> notesInSet = set
                    .SelectMany(point => notesByPoint[point])
                    .ToHashSet();

                bool allNotesAreSame = set.All(point =>
                {
                    var notes = notesByPoint[point];
                    return notes
                        .OrderBy(n => n)
                        .SequenceEqual(notesInSet.OrderBy(n => n));
                });
                return allNotesAreSame;
            });

            foreach (var set in setsWithSameNotes)
            {
                IEnumerable<Cell> cellsOutsideSet = cellGroup
                    .Where(c => !set.Contains(c.GridPoint));

                IEnumerable<int> notesToRemove = notesByPoint[set.First()];
                IEnumerable<Elimination> partialEliminations = cellsOutsideSet.SelectMany(c =>
                {
                    var eliminatedCellNotes = notesToRemove.Where(note => c.Notes.Contains(note));
                    return eliminatedCellNotes.Select(note => new Elimination(c.Row, c.Column, note));
                });
                eliminations.AddRange(partialEliminations);
            }
        }
        return eliminations;
    }
}
